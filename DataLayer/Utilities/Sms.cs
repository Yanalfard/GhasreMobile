using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Utilities
{
    public class Sms
    {
        public static async Task<bool> SendSms(string PhonNumber, string Message, string Temp)
        {
            var receptor = PhonNumber;
            var message = Message;
            var api = new Kavenegar.KavenegarApi("4D4B66616C686B64534544333856706F7A6A35793647497735395A79496C59485644345257546C615137303D");
            api.VerifyLookup(receptor, Message, Temp);
            return await Task.FromResult(true);
        }
    }
}
