using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplicationNEW
{
    public partial class Form1 : Form
    {
        /***********グローバル変数**************/
        string buttun_push ;//押されたボタンの数字
        string Operator = null;//押された演算子
        float result = 0;//計算結果
        float num1;
        float num2;
       // float exam;
        bool syousuu = false;//小数点フラグ
        float x = 1e30f;//floatの範囲
        float y = 1e-30f;//この範囲外はオーバーフロー

        public Form1()
        {
            InitializeComponent();
            label1.Text = "0";//スタートは0表示
        }

        /*******０～９または小数点が押されたとき*******/
        private void button20_Click(object sender, EventArgs e)
        {
            /*senderはobjectの型であり、押されたボタンの文字を取得することができないので、Button型に変形して処理する*/
            Button btn = (Button)sender;//sendorの情報を扱う（どのボタンが押されたかが分かるように）
            string text = btn.Text;//押されたボタンの数字、小数点をtextに代入

                    if((text=="0") && (syousuu == false) &&(buttun_push==null))//0連続表示しないようにする処理
                    {
                         label1.Text = "0";//0表示
                         return;
                    }
                    if ((text == "." ) && (syousuu == false))//小数点表示の処理
                    {
                            if (buttun_push == null)//「.5=0.5」と表示するときの処理
                            {
                                buttun_push ="0"+ text;//0を加えた後に小数点を連結する
                                label1.Text = buttun_push;//ラベルに数字を表示
                                syousuu = true;//フラグ立てる
                                return;
                            }
                            buttun_push += text;//入力された数字に小数点を連結する
                            label1.Text = buttun_push;//ラベルに数字を表示
                            syousuu = true;//フラグ立てる
                    }if ((text == ".") && (syousuu == true)) {//二回目以降は小数点表示を行わない
                                return;
                    }
            buttun_push += text;//入力された数字に連結する       
            label1.Text = buttun_push;//ラベルに数字を表示
            
            /* if (float.TryParse(buttun_push,out exam))
                 {
                   label1.Text = exam.ToString(); //ラベルに数字を表示
                }else{
                   label1.Text = "エラー";
                   }*/
        }

        /*******演算子または＝が押されたとき *******/
        private void button12_Click(object sender, EventArgs e)
        {
             num1 = result;
          
            if (buttun_push != null)//入力された数字がない場合は計算過程を飛ばす(＝+ や　＝×などの処理のため)
            {          
               num2= Convert.ToSingle(buttun_push);//文字型をdouble型に変換

                switch (Operator)
                {
                    case "+"://足し算が押されたとき
                        result = num1 + num2;
                        break;
                    case "-"://引き算が押されたとき
                        result = num1 - num2;
                        break;
                    case "*"://掛け算が押されたとき
                        result = num1 * num2;
                        break;
                    case "/"://割り算が押されたとき
                        result = num1 / num2;
                        if (num2 == 0) {           //0の割り算のとき
                            label1.Text = "エラー";
                            return;}
                        break;
                    default://演算子が押されてなかったとき、入力文字をそのままresultに入れる
                        result = num2;
                        break;
                }
            }

            if ((result >= x) || (result <= y))//floatの範囲外
            {
                label1.Text = "エラー";//エラー表示
                buttun_push = null;//入力クリア
                Operator = null;//演算子クリア
                result = 0;//結果クリア
                syousuu = false;//小数点フラグリセット
            }
            else//範囲内
            {
                label1.Text = result.ToString();//resultをlabel1に表示
                buttun_push = null;//入力数字のリセット
                Button btn = (Button)sender;//数字の時と同様にsenderをbtnに代入
                Operator = btn.Text;//演算子をoperatorに入れる
                syousuu = false;//小数点フラグリセット
                    if (Operator == "=")//operatorが「＝」であるとき
                    {
                        Operator = null;//演算子をクリア
                    }
            }
        }

        private void button16_Click(object sender, EventArgs e) //CAが押されたとき
        {
            buttun_push = null;//入力クリア
            Operator = null;//演算子クリア
            result = 0;//結果クリア
            syousuu = false;//小数点フラグリセット
            label1.Text ="0";//0表示
        }
     
        private void button17_Click(object sender, EventArgs e)//CEが押されたとき
        {
            buttun_push = null;//入力クリア
            syousuu = false;//小数点フラグリセット
            label1.Text = "0";//0表示
        }
    }
}

