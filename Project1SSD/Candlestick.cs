using System;
using System.Drawing;

public class Candlestick
{
    public float Open { get; set; }
    public float High { get; set; }
    public float Low { get; set; }
    public float Close { get; set; }
    public float Volume { get; set; }
    public int X { get; set; }  // X position for drawing
    public int Width { get; set; } = 10;  // Default width of the candlestick

    public Candlestick(float open, float high, float low, float close, float volume, int x)
    {
        Open = open;
        High = high;
        Low = low;
        Close = close;
        Volume = volume;
        X = x;
    }

    public Candlestick(string data)
    {
        var separators = new char[] { ',' };
        var values = data.Split(separators);

        if (values.Length != 6)
        {

        }

        Date = DateTime.ParseExact(values[0], "yyyy-MM-dd", CultureInfo.InvariantCulture);
        Open = decimal.Parse(values[1]);
        High = decimal.Parse(values[2]);
        Low = decimal.Parse(values[3]);
        Close = decimal.Parse(values[4]);
        Volume = decimal.Parse(values[5]);
    }

    public void Draw(Graphics g, int yOffset, float scale)
    {
        // Determine color: Green for bullish, Red for bearish
        Color candleColor = (Close >= Open) ? Color.Green : Color.Red;
        using (Pen wickPen = new Pen(Color.Black, 2))
        using (Brush candleBrush = new SolidBrush(candleColor))
        {
            // Scale values for drawing
            int highY = yOffset - (int)(High * scale);
            int lowY = yOffset - (int)(Low * scale);
            int openY = yOffset - (int)(Open * scale);
            int closeY = yOffset - (int)(Close * scale);

            // Draw wick (high to low)
            g.DrawLine(wickPen, X + Width / 2, highY, X + Width / 2, lowY);

            // Draw candlestick body
            int bodyTop = Math.Min(openY, closeY);
            int bodyHeight = Math.Abs(closeY - openY);
            g.FillRectangle(candleBrush, X, bodyTop, Width, Math.Max(1, bodyHeight)); // Avoid 0-height
            g.DrawRectangle(Pens.Black, X, bodyTop, Width, Math.Max(1, bodyHeight));
        }
    }
}
