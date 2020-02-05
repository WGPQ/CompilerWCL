using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompilerWCL.Principal
{
    public partial class Form3 : Form
    {

        public static RichTextBox r1;
        
        public  Form3()
        {
            InitializeComponent();
            r1 = rich_Editor;
        }

        public static void llenarCode(String code)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
           
           LineNumberTextBox.Font = rich_Editor.Font;
          rich_Editor.Select();
            AddLineNumbers();
        }
        public int getWidth()
        {
            int w = 25;
            // get total lines of richTextBox1    
            int line = rich_Editor.Lines.Length;

            if (line <= 99)
            {
                w = 20 + (int)rich_Editor.Font.Size;
            }
            else if (line <= 999)
            {
                w = 30 + (int)rich_Editor.Font.Size;
            }
            else
            {
                w = 50 + (int)rich_Editor.Font.Size;
            }

            return w;
        }

        public void AddLineNumbers()
        {
            // create & set Point pt to (0,0)    
            Point pt = new Point(0, 0);
            // get First Index & First Line from richTextBox1    
            int First_Index = rich_Editor.GetCharIndexFromPosition(pt);
            int First_Line = rich_Editor.GetLineFromCharIndex(First_Index);
            // set X & Y coordinates of Point pt to ClientRectangle Width & Height respectively    
            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;
            // get Last Index & Last Line from richTextBox1    
            int Last_Index = rich_Editor.GetCharIndexFromPosition(pt);
            int Last_Line = rich_Editor.GetLineFromCharIndex(Last_Index);
            // set Center alignment to LineNumberTextBox    
            LineNumberTextBox.SelectionAlignment = HorizontalAlignment.Center;
            // set LineNumberTextBox text to null & width to getWidth() function value    
            LineNumberTextBox.Text = "";
            LineNumberTextBox.Width = getWidth();
            // now add each line number to LineNumberTextBox upto last line    
            for (int i = First_Line; i <= Last_Line + 1; i++)
            {
                LineNumberTextBox.Text += i + 1 + "\n";
            }
        }


        /*public static void AddLineNumbers2()
        {
            // create & set Point pt to (0,0)    
            Point pt = new Point(0, 0);
            // get First Index & First Line from richTextBox1    
            int First_Index = r1.GetCharIndexFromPosition(pt);
            int First_Line = r1.GetLineFromCharIndex(First_Index);
            // set X & Y coordinates of Point pt to ClientRectangle Width & Height respectively    
            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;
            // get Last Index & Last Line from richTextBox1    
            int Last_Index = r1.GetCharIndexFromPosition(pt);
            int Last_Line = r1.GetLineFromCharIndex(Last_Index);
            // set Center alignment to LineNumberTextBox    
            LineNumberTextBox.SelectionAlignment = HorizontalAlignment.Center;
            // set LineNumberTextBox text to null & width to getWidth() function value    
            LineNumberTextBox.Text = "";
            LineNumberTextBox.Width = getWidth();
            // now add each line number to LineNumberTextBox upto last line    
            for (int i = First_Line; i <= Last_Line + 1; i++)
            {
                LineNumberTextBox.Text += i + 1 + "\n";
            }
        }*/

        private void rich_Editor_FontChanged(object sender, EventArgs e)
        {
            LineNumberTextBox.Font = rich_Editor.Font;
            rich_Editor.Select();
            AddLineNumbers();
           
        }

        private void LineNumberTextBox_MouseDown(object sender, MouseEventArgs e)
        {
           rich_Editor.Select();
            LineNumberTextBox.DeselectAll();
        }

        private void rich_Editor_TextChanged(object sender, EventArgs e)
        {
            if (rich_Editor.Text == "")
            {
                AddLineNumbers();
            }
        }

        private void rich_Editor_VScroll(object sender, EventArgs e)
        {
            LineNumberTextBox.Text = "";
            AddLineNumbers();
           LineNumberTextBox.Invalidate();
        }

        private void rich_Editor_SelectionChanged(object sender, EventArgs e)
        {
            Point pt = rich_Editor.GetPositionFromCharIndex(rich_Editor.SelectionStart);
            if (pt.X == 1)
            {
                AddLineNumbers();
            }
        }

       
       
    }
}
