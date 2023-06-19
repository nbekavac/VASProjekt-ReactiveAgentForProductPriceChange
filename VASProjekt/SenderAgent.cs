using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActressMas;

namespace VASProjekt
{
    public class SenderAgent : Agent
    {
        private string _mobileModel;
        private string _mobileColor;
        private string _mobiles;
        public SenderAgent(string mobileModel, string mobileColor)
        {
            _mobileModel = mobileModel;
            _mobileColor = mobileColor;
        }

        public override void Setup()
        {
            _mobiles = _mobiles + " " + _mobileModel;
            _mobiles = _mobiles + ",";
            _mobiles = _mobiles + "" + _mobileColor;
        }

        public override void Act(Message message)
        {
            message.Parse(out string action, out string parameters);
            Send("receivingAgent", $"{_mobiles}");

            Stop();
        }

       
    }

  

   
}
