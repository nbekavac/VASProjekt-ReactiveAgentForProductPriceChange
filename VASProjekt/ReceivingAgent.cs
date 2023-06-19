using ActressMas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VASProjekt
{
    public class ReceivingAgent: Agent
    {
        private List<string> _mobiles;
        private List<string> _mobilesWithoutComma;
        public ReceivingAgent()
        {
            _mobiles = new List<string>();
            _mobilesWithoutComma = new List<string>();
        }

        public override void Setup()
        {
            Broadcast("start");

        }

        public override void Act(Message message)
        {
            message.Parse(out string action, out string parameters);
            _mobiles.Add(parameters);

        }

        public override void ActDefault()
        {
            Scraper scraper= new Scraper();
            _mobilesWithoutComma = _mobiles.First().Split(',').ToList();
            scraper.GetProductFrame(_mobilesWithoutComma.First(), _mobilesWithoutComma.Last());
            Stop();
            
            
            
        }

        
    }
}
