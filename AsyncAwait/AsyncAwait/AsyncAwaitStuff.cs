using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AsyncAwait
{
    [TestFixture]
    public class ThrowingExceptions
    {

        [Test]
        public async Task ThrowException_FromTask()
        {
            //handles async exceptions
            Assert.That(TaskThrowsException().Wait, Throws.TypeOf<AggregateException>());
        }

        [Test]
        public async Task ThrowMultipleException_FromTask()
        {
            //handles async exceptions
            Assert.That(MultipleTasksThrowExceptions().Wait, Throws.TypeOf<AggregateException>());
        }

        [Test]
        public async Task ThrowException_FromAsyncMethod2()
        {
            //cannot handle async exceptions
            Assert.That(() => MethodThrowsException(), Throws.Nothing);
        }

        private async void MethodThrowsException()
        {
            await Task.Yield();
            throw new Exception();
        }

        private async Task MultipleTasksThrowExceptions()
        {
            for (int i = 0; i < 2; i++)
                await TaskThrowsException();
        }

        private async Task TaskThrowsException()
        {
            await Task.Yield();
            throw new Exception();
        }
    }


}
