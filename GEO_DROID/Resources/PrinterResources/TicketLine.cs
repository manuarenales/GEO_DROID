

namespace GEO_DROID.Resources.PrinterResources
{
    using System;
    using System.Text;

    public class TicketLine
    {
        private const char ESC = '\x1b';
        private const char GS = '\x1d';
        private const char LF = '\x1d';

        private StringBuilder commands;
        public string Text { get; set; }

        public TicketLine(string text)
        {
            Text = text;
            commands = new StringBuilder();
        }

        public TicketLine Bold(bool on = true)
        {
            commands.Append($"{ESC}E{(char)(on ? 1 : 0)}");
            return this;
        }

        public TicketLine Underline(bool on = true)
        {
            commands.Append($"{ESC}-{(char)(on ? 1 : 0)}");
            return this;
        }

        public TicketLine Invert(bool on = true)
        {
            commands.Append($"{GS}B{(char)(on ? 1 : 0)}");
            return this; //invierte el orden de escritura del texto 

        }

        public TicketLine DoubleHeight(bool on = true)
        {
            commands.Append($"{ESC}!{(char)(on ? 16 : 0)}");
            return this;
        }

        public TicketLine DoubleWidth(bool on = true)
        {
            commands.Append($"{ESC}!{(char)(on ? 32 : 0)}");
            return this;
        }

        public TicketLine Font(char font = 'A')
        {
            int fontType = font == 'B' ? 1 : 0;
            commands.Append($"{ESC}M{(char)fontType}");
            return this;
        }

        public TicketLine Align(string alignment = "left")
        {
            int alignType = alignment.ToLower() switch
            {
                "center" => 1,
                "right" => 2,
                _ => 0,
            };
            commands.Append($"{ESC}a{(char)alignType}");
            return this;
        }

        public TicketLine LineSpacing(int? spacing = null)
        {
            if (spacing.HasValue)
            {
                commands.Append($"{ESC}3{(char)spacing.Value}");
            }
            else
            {
                commands.Append($"{ESC}2");
            }
            return this;
        }

        public string Build()
        {
            return commands.ToString() + Text + "\n";
        }
    }

}
