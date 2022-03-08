using System;
using System.Linq;

namespace Blinkay.Domain
{
    public class Generic
    {
        public virtual int Id { get; set; }
        public virtual string Texto { get; set; }
        public virtual void SetRandomText()
        {
            // TODO: move to utils
            Random rnd = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string randString = new string(Enumerable.Repeat(chars, 5)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());
            Texto = randString;
        }
    }
}
