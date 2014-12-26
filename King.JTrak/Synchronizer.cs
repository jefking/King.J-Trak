namespace King.JTrak
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Synchronizer : ISynchronizer
    {
        public async Task Run()
        {
            await new TaskFactory().StartNew(() => { });
        }
    }
}