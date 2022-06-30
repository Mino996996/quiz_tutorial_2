using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp_tutorial_2
{
    public partial class Form1 : Form
    {
        Random randomizer = new Random();
        int addend1;
        int addend2;
        int minuend;
        int subtrahend;
        int multiplicand;
        int multiplier;
        int dividend;
        int divisor;
        int timeLeft;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 各項に数字を充てて、タイマーを開始させる
        /// </summary>
        public void StartTheQuiz()
        {
            // 問題式の左辺を表示する
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);

            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();

            sum.Value = 0;

            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            // 制限時間設定
            timeLeft = 30;
            timeLabel.Text = "30 seconds";
            timer1.Start();
        }

        /// <summary>
        /// ボタンを押したらStartTheQuiz()を実行してボタンを押せなくする
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            button1.Enabled = false;
        }

        /// <summary>
        /// 全て正解するまでの残り時間を表示する
        /// </summary>
        /// <returns>全入力が正解ならTrue/不正解ならFalse</returns>
        private bool CheckTheAnswer()
        {
            return ((addend1 + addend2 == sum.Value) && (minuend - subtrahend == difference.Value) && (multiplicand * multiplier == product.Value) && (dividend / divisor == quotient.Value));
        }

        /// <summary>
        /// インターバル1000msで設定中。呼び出されたら以降は毎秒実行される
        /// 全問正解orタイムオーバーで定期実行は終わる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(CheckTheAnswer())
            {
                timer1.Stop();
                MessageBox.Show("全問正解です！");
                button1.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + "[秒]";
            }
            else
            {
                // 制限時間越えの時
                timer1.Stop();
                timeLabel.Text = "Time up!";
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                button1.Enabled = true;
            }
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            // Select the whole answer in the NumericUpDown control.
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }
    }
}
