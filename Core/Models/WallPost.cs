using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class WallPost
    {
        public WallPost(string text, DateTime dateTime)
        {
            Text = text;
            DateTime = dateTime;
        }

        public string Text { get; set; }

        public DateTime DateTime { get; set; }
    }
}