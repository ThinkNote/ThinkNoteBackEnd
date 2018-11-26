using System;
using System.Collections.Generic;
using System.Text;
using ThinkNoteBackEnd.User;
using Xunit;

namespace ThinkNoteBackEnd.MainTest
{
    public class Class1
    {
        [Fact]
        public void Test_IdWorker()
        {
            //it shoult generate an id.
            var worker = new IdWorker();


            Assert.True(worker.NextId() > 0);
            Assert.True(worker.NextId().ToString().Length == 18);
        }

    }
}
