using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActressMas;
using System.Threading;

namespace VASProjekt
{
    public class PriceAgent: Agent
    {
        private string _mobileModel;
        private string _mobileColor;
        private string _wantedPrice;
        private string _returnedPrice;
        public PriceAgent(string mobileModel, string mobileColor, string wantedPrice)
        {
            _mobileModel = mobileModel;
            _mobileColor = mobileColor;
            _wantedPrice = wantedPrice;
        }

        public override void Setup()
        {
            Broadcast("start");
        }

        public override void Act(Message message)
        {

        }

        public override void ActDefault()
        {
            ExtractDataWithPrices extractDataWithPrices= new ExtractDataWithPrices();
            _returnedPrice=extractDataWithPrices.ExtractingDataFromFile(_mobileModel,_mobileColor, _wantedPrice);
            Thread.Sleep(5000);
            if(_returnedPrice != null)
            {
                Stop();
            }
            
        }
    }
}
