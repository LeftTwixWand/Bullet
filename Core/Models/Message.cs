using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Core.Models
{
    public class Message
    {
        public string Sender { get; set; }
        
        public string Text { get; set; }

        public DateTime DateTime { get; set; }

        public HorizontalAlignment MsgAlignment { get; set; }

        public SolidColorBrush BgColor { get; set; }
    
        public Message(string sender, string text, HorizontalAlignment alignment)
        {
            Sender = sender;
            Text = text;
            MsgAlignment = alignment;
            DateTime = DateTime.Now;
            if (alignment == HorizontalAlignment.Left || alignment == HorizontalAlignment.Center)
            {
                BgColor = (SolidColorBrush)Application.Current.Resources["SystemControlBackgroundChromeMediumBrush"];
            }
            else
            {
                BgColor = (SolidColorBrush)Application.Current.Resources["SystemControlBackgroundChromeMediumBrush"];
            }
        }




        //public MessageType MessageType { get; set; }

        //public byte[] Date { get; set; }

        //public Message(string sender, DateTime dateTime, MessageType messageType, byte[] date)
        //{
        //    Sender = sender;
        //    DateTime = dateTime;
        //    MessageType = messageType;
        //    Date = date;
        //}
    }
}