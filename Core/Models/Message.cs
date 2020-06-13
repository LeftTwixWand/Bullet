using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Message
    {
        public string Sender { get; set; }
        
        public DateTime DateTime { get; set; }
        
        public MessageType MessageType { get; set; }
        
        public byte[] Date { get; set; }
        
        public Message(string sender, DateTime dateTime, MessageType messageType, byte[] date)
        {
            Sender = sender;
            DateTime = dateTime;
            MessageType = messageType;
            Date = date;
        }
    }
}