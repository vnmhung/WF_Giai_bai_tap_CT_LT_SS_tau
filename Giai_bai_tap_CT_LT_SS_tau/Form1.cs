using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Giai_bai_tap_CT_LT_SS_tau
{
    public partial class Form1 : Form
    {
        double gamma = 1.025, l = 66.0, b = 12.0, t_m = 4.1, t_l = 3.6, alpha = 0.87,
                  x_f = -1.5, mct_1cm = 70.0, p_n = 170.0, x_p = -25.0, 
                  t_m1, t_l1, p_c = 15.0, delta_x, 
                  tpc, delta_t, m_ch1, m_ch2, d_delta_1, d_delta_2, delta_1, delta_2;

        double b2_p = 3500.0, b2_tpc = 10, b2_t = 4.6, b2_teta_0 = 6.0, b2_h_0 = 0.6,
                b2_pn = 100.0, b2_yp = -1.8, b2_zp = 3.0, b2_t_1, b2_teta_1, b2_pc = 6.0,
                b2_y_1 = 2.2, b2_z_1 = 3.3, b2_y_2 = -2.2, b2_z_2 = 1.0, b2_teta_2;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox_L.Text = l.ToString();
            textBox_B.Text = b.ToString();
            textBox_Tm.Text = t_m.ToString();
            textBox_TL.Text = t_l.ToString();
            textBox_alpha.Text = alpha.ToString();
            textBox_xF.Text = x_f.ToString();
            textBox_MCT_1cm.Text = mct_1cm.ToString();
            textBox_pn.Text = p_n.ToString();
            textBox_xP.Text = x_p.ToString();
            textBox_pc.Text = p_c.ToString();

            textBox_2_p.Text = b2_p.ToString();
            textBox_2_tpc.Text = b2_tpc.ToString();
            textBox_2_t.Text = b2_t.ToString();
            textBox_2_teta_0.Text = b2_teta_0.ToString();
            textBox_2_h_0.Text = b2_h_0.ToString();
            textBox_2_pn.Text = b2_pn.ToString();
            textBox_2_yp.Text = b2_yp.ToString();
            textBox_2_zp.Text = b2_zp.ToString();
            textBox_2_pc.Text = b2_pc.ToString();
            textBox_2_y_1.Text = b2_y_1.ToString();
            textBox_2_z_1.Text = b2_z_1.ToString();
            textBox_2_y_2.Text = b2_y_2.ToString();
            textBox_2_z_2.Text = b2_z_2.ToString();
        }

        bool test = false;
        private void timer100ms_Tick(object sender, EventArgs e)
        {
            if (test) return;
            try
            {
                process_1();
                textBox_Tm_1.ForeColor = Color.Green;
                textBox_TL_1.ForeColor = Color.Green;
                textBox_delta_x.ForeColor = Color.Green;
            }
            catch (Exception ee)
            {
                textBox_Tm_1.Text = "- - -";
                textBox_TL_1.Text = "- - -";
                textBox_delta_x.Text = "- - -";
                textBox_Tm_1.ForeColor = Color.Red;
                textBox_TL_1.ForeColor = Color.Red;
                textBox_delta_x.ForeColor = Color.Red;
            }

            try
            {
                process_2();
                textBox_2_t_1.ForeColor = Color.Green;
                textBox_2_teta_1.ForeColor = Color.Green;
                textBox_2_teta_2.ForeColor = Color.Green;
            }
            catch (Exception ee)
            {
                textBox_2_t_1.Text = "- - -";
                textBox_2_teta_1.Text = "- - -";
                textBox_2_teta_2.Text = "- - -";
                textBox_2_t_1.ForeColor = Color.Red;
                textBox_2_teta_1.ForeColor = Color.Red;
                textBox_2_teta_2.ForeColor = Color.Red;
            }
        }

        private void process_1()
        {
            //get values
            l = Convert.ToDouble(textBox_L.Text);
            b = Convert.ToDouble(textBox_B.Text);
            t_m = Convert.ToDouble(textBox_Tm.Text);
            t_l = Convert.ToDouble(textBox_TL.Text);
            alpha = Convert.ToDouble(textBox_alpha.Text);
            x_f = Convert.ToDouble(textBox_xF.Text);
            mct_1cm = Convert.ToDouble(textBox_MCT_1cm.Text);
            p_n = Convert.ToDouble(textBox_pn.Text);
            x_p = Convert.ToDouble(textBox_xP.Text);
            p_c = Convert.ToDouble(textBox_pc.Text);


            tpc = gamma * alpha * l * b;

            delta_t = p_n / tpc;//gia số mớn nước
            m_ch1 = p_n * (x_p - x_f);//mômen gây chúi do nhận hàng

            d_delta_1 = m_ch1 / (100 * mct_1cm);//gia số độ chúi do nhận hàng

            t_m1 = t_m + delta_t + (l / 2 - x_f) * (d_delta_1 / l);//mớn nước mũi sau nhận hàng
            t_l1 = t_l + delta_t - (l / 2 + x_f) * (d_delta_1 / l);//mớn nước lái sau nhận hàng

            //tính độ chúi sau nhận hàng
            delta_1 = t_m1 - t_l1;
            delta_2 = 0; // (= t_m2 - t_l2)  mà 2 cái này bằng nhau
            d_delta_2 = delta_2 - delta_1;

            //tính khoảng cách dịch chuyển hàng
            delta_x = (d_delta_2 * 100 * mct_1cm) / p_c;

            //tính khoảng cách dịch chuyển hàng
            //m_n2= 8*del

            textBox_Tm_1.Text = (Math.Truncate(t_m1 * 100) / 100).ToString();
            textBox_TL_1.Text = (Math.Truncate(t_l1 * 100) / 100).ToString();
            textBox_delta_x.Text = (Math.Truncate(delta_x * 100) / 100).ToString();
        }

        private void process_2()
        {
            //get values
            b2_p = Convert.ToDouble(textBox_2_p.Text);
            b2_tpc = Convert.ToDouble(textBox_2_tpc.Text);
            b2_t = Convert.ToDouble(textBox_2_t.Text);
            b2_teta_0 = Convert.ToDouble(textBox_2_teta_0.Text);
            b2_h_0 = Convert.ToDouble(textBox_2_h_0.Text);
            b2_pn = Convert.ToDouble(textBox_2_pn.Text);
            b2_yp = Convert.ToDouble(textBox_2_yp.Text);
            b2_zp = Convert.ToDouble(textBox_2_zp.Text);
            b2_pc = Convert.ToDouble(textBox_2_pc.Text);
            b2_y_1 = Convert.ToDouble(textBox_2_y_1.Text);
            b2_z_1 = Convert.ToDouble(textBox_2_z_1.Text);
            b2_y_2 = Convert.ToDouble(textBox_2_y_2.Text);
            b2_z_2 = Convert.ToDouble(textBox_2_z_2.Text);

            double b2_delta_t = b2_pn / (100 * b2_tpc);
            b2_t_1 = b2_t + b2_delta_t;

            double b2_p_1 = b2_p + b2_pn;

            double b2_delta_h_n = (b2_pn / b2_p_1) * (b2_t + b2_delta_t / 2 - b2_h_0 - b2_zp);
            double b2_h_1 = b2_h_0 + b2_delta_h_n;

            double b2_m_n1 = b2_pn * b2_yp;

            double b2_d_teta_1 = Math.Atan(b2_m_n1 / (b2_p_1 * b2_h_1)) * 57.2957795;

            //MessageBox.Show(b2_d_teta_1.ToString());
            //test = true;
            b2_teta_1 = b2_teta_0 + b2_d_teta_1;

            double b2_delta_h_c = -1* b2_pc * (b2_z_2 - b2_z_1) / b2_p_1;
            double b2_h_2 = b2_h_1 + b2_delta_h_c;

            double b2_m_n2 = b2_pc * (b2_y_2 - b2_y_1);

            double b2_d_teta_2 = Math.Atan(b2_m_n2 / (b2_p_1 * b2_h_2)) * 57.2957795;
            b2_teta_2 = b2_teta_1 + b2_d_teta_2;

            textBox_2_t_1.Text = (Math.Truncate(b2_t_1 * 10000) / 10000).ToString();
            textBox_2_teta_1.Text = (Math.Truncate(b2_teta_1 * 10000) / 10000).ToString();
            textBox_2_teta_2.Text = (Math.Truncate(b2_teta_2 * 10000) / 10000).ToString();
        }
        
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://vnmhung.netlify.com/");
        }

    }
}
