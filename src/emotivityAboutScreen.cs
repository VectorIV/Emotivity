using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace emotivityMain
{
    public partial class emotivityAboutScreen : Form
    {
        public emotivityAboutScreen()
        {
            InitializeComponent();

            creditBox_AutoScroll();
        }

        private void creditBox_AutoScroll()
        {
            creditBox.Text = "Credits:\r\n\r\n"
                + "Emotiv Team - Backgroud image, Emotiv logo,\r\n"
                + "headset status icons, EPOC icon, designs.\r\n"
                + "\r\n"
                + "Setyo Ari Wibowo - Emoticons.\r\n"
                + "Herbert Spencer - Cloud thought art.\r\n"
                + "Gregor Cresnar - Question marks icon art.\r\n"
                + "\r\n"
                + "Mark Shorter - Bar charts icon art, banner art.\r\n"
                + "Joel McKinney - Graph with line graph art.\r\n"
                + "AFY Studio - Setting gear icon and button art.\r\n"
                + "\r\n"
                + "Rockicon - Information icon.\r\n"
                + "Christopher T. Howlett - Time icon art.r\n"
                + "Dmitriy Ivanov - Selection icon.\r\n"
                + "\r\n"
                + "Viktor Vorobyev - Cancel icon.\r\n"
                + "dilayorganci - Save file icon.\r\n"
                + "Icons8 - Eraser icon art.\r\n"
                + "\r\n"
                + "Thengakola - Bin icon art.\r\n"
                + "Gregor Cresnar - User icon art.\r\n"
                + "Miguel C Balandrano - Eye movement icons.\r\n"
                + "\r\n"
                + "Daniela Baptista - Brow icon.\r\n"
                + "Cenk ÇAKMAK - Teeth clenching icon.\r\n"
                + "André Luiz - Smiling and smirking icons.\r\n"
                + "\r\n"
                + "chiara galli - Laughing face icon.\r\n"
                + "Arthur Shlain - TXT and LOG icon arts.\r\n"
                + "Shashwalthy - Recycle arrows icon.\r\n"
                + "\r\n"
                + "To Uyen - Human head with brain icon.\r\n"
                + "\r\n"
                + "\r\n"
                + "\r\n"
                + "\r\n\r\n\r\n\r\n\r\n\r\n";
            creditBoxScrollTimer.Start();
        }

        private void creditBoxScrollTimer_Tick(object sender, EventArgs e)
        {
            PostMessage(creditBox.Handle, WM_VSCROLL, (IntPtr)SB_LINEDOWN, (IntPtr)IntPtr.Zero);
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        [DllImport("user32.dll")]
        static extern int PostMessage(IntPtr wnd, uint Msg, IntPtr wParam, IntPtr lParam);
        const uint WM_VSCROLL = 0x0115;
        const uint SB_LINEDOWN = 3;
    }
}
