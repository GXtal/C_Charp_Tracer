using ConsoleApp;
using TracerLib;
namespace TracerTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Test_OneEmptyThread()
        {
            // Arrange
            var tracer = new Tracer();

            // Act
            tracer.StartTrace();
            Thread.Sleep(600);
            tracer.StopTrace();
            var traceResult = tracer.GetTraceResult();

            // Assert (trace res)
            var thInfo = traceResult.ThreadsInfo;
            Assert.Multiple(() =>
            {
                Assert.That(thInfo.Count, Is.EqualTo(1));
                Assert.That(thInfo[0].Time, Is.InRange(600, 700));
                Assert.That(thInfo[0].CompleteMethods.Count, Is.EqualTo(1));
            });
        }

        [Test]
        public void Test_SingleThread_SingleSimpleObject()
        {
            // Arrange
            var tracer = new Tracer();

            var foo = new SimpleClass(tracer);
            // Act

            foo.InnerFunction1();
            var traceResult = tracer.GetTraceResult();

            // Assert (trace res)
            var threads = traceResult.ThreadsInfo;
            Assert.That(threads.Count, Is.EqualTo(1));
            Assert.That(threads[0].CompleteMethods.Count, Is.EqualTo(1));

            var threadChilds = threads[0].CompleteMethods;
            Assert.Multiple(() =>
            {
                Assert.That(threadChilds[0].Time, Is.InRange(400, 500)); // time
                Assert.That(threadChilds[0].ChildMethods.Count, Is.EqualTo(0)); // count of first
                Assert.That(threadChilds[0].Name, Is.EqualTo("InnerFunction1"));
                Assert.That(threadChilds[0].ClassName, Is.EqualTo("SimpleClass"));
            });
        }
        [Test]
        public void Test_SingleThread_1ComplexObject_1SimpleObject()
        {
            // Arrange
            var tracer = new Tracer();

            var foo = new SimpleClass(tracer);
            var boo = new ComplexClass(tracer);
            // Act

            foo.InnerFunction2();
            boo.OutsideFunction();
            var traceResult = tracer.GetTraceResult();

            // Assert (trace res)
            var threads = traceResult.ThreadsInfo;
            Assert.That(threads.Count, Is.EqualTo(1));
            Assert.That(threads[0].CompleteMethods.Count, Is.EqualTo(2));

            var threadChilds = threads[0].CompleteMethods;
            Assert.Multiple(() =>
            {
                Assert.That(threads[0].Time, Is.InRange(800, 1000));
                Assert.That(threadChilds[0].Time, Is.InRange(100, 200)); // time
                Assert.That(threadChilds[0].ChildMethods.Count, Is.EqualTo(0)); // count of first
                Assert.That(threadChilds[1].Time, Is.InRange(700, 800)); // time
                Assert.That(threadChilds[1].ChildMethods.Count, Is.EqualTo(2)); // count of second
                Assert.That(threadChilds[0].Name, Is.EqualTo("InnerFunction2"));
                Assert.That(threadChilds[0].ClassName, Is.EqualTo("SimpleClass"));
                Assert.That(threadChilds[1].Name, Is.EqualTo("OutsideFunction"));
                Assert.That(threadChilds[1].ClassName, Is.EqualTo("ComplexClass"));
                Assert.That(threadChilds[1].ChildMethods[0].Name, Is.EqualTo("InnerFunction2"));
                Assert.That(threadChilds[1].ChildMethods[1].Name, Is.EqualTo("InnerFunction1"));
            });
        }

        [Test]
        public void Test_SingleThread_1RecursFunction()
        {
            // Arrange
            var tracer = new Tracer();
            var boo = new ComplexClass(tracer);
            // Act
            boo.RecursFunction();
            var traceResult = tracer.GetTraceResult();
            // Assert
            var thInfo = traceResult.ThreadsInfo;
            Assert.Multiple(() =>
            {
                Assert.That(thInfo.Count, Is.EqualTo(1));
                Assert.That(thInfo[0].Time, Is.InRange(200,300));
                Assert.That(thInfo[0].CompleteMethods.Count, Is.EqualTo(1));
                Assert.That(thInfo[0].CompleteMethods[0].Time, Is.InRange(200, 300));
                Assert.That(thInfo[0].CompleteMethods[0].ChildMethods[0].Time, Is.InRange(100, 200));
                Assert.AreEqual(thInfo[0].CompleteMethods[0].Name, thInfo[0].CompleteMethods[0].ChildMethods[0].Name);
                Assert.AreEqual(thInfo[0].CompleteMethods[0].Name, thInfo[0].CompleteMethods[0].ChildMethods[0].ChildMethods[0].Name);
            });
        }
        [Test]
        public void Test_2Threds_1SimpleObject()
        {
            // Arrange
            var tracer = new Tracer();

            var bar = new SimpleClass(tracer);
            // Act
            var thread1 = new Thread(() =>
            {
                bar.InnerFunction2();
            });
            thread1.Start();

            var thread2 = new Thread(() =>
            {
                bar.InnerFunction1();
                bar.InnerFunction1();
            });
            thread2.Start();

            thread1.Join();
            thread2.Join();

            var traceResult = tracer.GetTraceResult();

            // Assert (trace res)
            var threadsInfo = traceResult.ThreadsInfo;
            Assert.That(threadsInfo.Count, Is.EqualTo(2));

            var thread1Childs = threadsInfo[0].CompleteMethods;
            Assert.That(thread1Childs.Count, Is.EqualTo(1));
            Assert.That(threadsInfo[0].Time, Is.InRange(100, 200));
            Assert.Multiple(() =>
            {
                Assert.That(thread1Childs[0].Time, Is.InRange(100, 200));
                Assert.That(thread1Childs[0].ChildMethods.Count, Is.EqualTo(0)); // count of first
                Assert.That(thread1Childs[0].ClassName, Is.EqualTo("SimpleClass"));
                Assert.That(thread1Childs[0].Name, Is.EqualTo("InnerFunction2"));
            });

            var thread2Childs = threadsInfo[1].CompleteMethods;
            Assert.That(thread2Childs.Count, Is.EqualTo(2));
            Assert.That(threadsInfo[1].Time, Is.InRange(800, 1000));
            Assert.Multiple(() =>
            {
                Assert.That(thread2Childs[0].Time, Is.InRange(400, 500));
                Assert.That(thread2Childs[0].ChildMethods.Count, Is.EqualTo(0)); // count of first
                Assert.That(thread2Childs[0].ClassName, Is.EqualTo("SimpleClass"));
                Assert.That(thread2Childs[0].Name, Is.EqualTo("InnerFunction1"));
            });


        }
    }
}