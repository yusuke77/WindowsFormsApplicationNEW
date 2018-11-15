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
        string buttun_push = null ;         // 押されたボタンの数字
        string Operator = null;             // 押された演算子
        string clr = null;                  // clrボタンの文字判別
        float result = 0;                   // 計算結果
        float num1 = 0;                     // 左辺
        float num2 = 0;                     // 右辺
        float max_float = 1e30f;            // floatの範囲
        float min_float = -1e30f;           // この範囲外はオーバーフロー
        bool syousuu = false;               // 小数点フラグ

        public Form1()
        {
            InitializeComponent();
            label1.Text = "0";              
        }

        /*******０～９または小数点が押されたとき*******/
        private void button_num_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;     // sendorの情報を扱う（どのボタンが押されたかが分かるように）
            string text = btn.Text;          // 押されたボタンの数字、小数点をtextに代入

                    if((text=="0") && (syousuu == false) &&(buttun_push==null))     // 0連続表示しないようにする処理
                    {
                         label1.Text = "0";     
                         buttun_push += text;   // 入力された数字に連結する     
                         return;
                    }
                    if ((text == "." ) && (syousuu == false))   // 小数点表示の処理
                    {
                            if (buttun_push == null)        // 「.5=0.5」と表示するときの処理
                            {
                                buttun_push ="0"+ text;     // 0を加えた後に小数点を連結する
                                label1.Text = buttun_push;  
                                syousuu = true;             // フラグ立てる
                                return;
                            }
                                buttun_push += text;        // 入力された数字に小数点を連結する
                                label1.Text = buttun_push; 
                                syousuu = true;                 
                    }
                    if ((text == ".") && (syousuu == true)) {     // 二回目以降は小数点表示を行わない
                                return;
                    }
            buttun_push += text;        // 入力された数字に連結する       
            label1.Text = buttun_push;  // ラベルに数字を表示
        }

        /*******演算子または＝が押されたとき *******/
        private void button_enzansi_Click(object sender, EventArgs e)
        {
            num1 = result;
          
            if (buttun_push != null)    // 入力された数字がない場合は計算過程を飛ばす(＝+ や　＝×などの処理のため)
            {          
               num2= Convert.ToSingle(buttun_push);     // 文字型をfloat型に変換

                switch (Operator)
                {
                    case "＋":       
                        result = num1 + num2;
                        break;
                    case "－":       
                        result = num1 - num2;
                        break;
                    case "×":       
                        result = num1 * num2;
                        break;
                    case "÷":       
                        result = num1 / num2;

                        if (num2 == 0) {           // 0の割り算のとき
                            label1.Text = "エラー";
                            buttun_push = null;     
                            Operator = null;       
                            result = 0;             
                            syousuu = false;        
                            return;
                        }
                        break;
                    default:            // 演算子が押されてなかったとき、入力文字をそのままresultに入れる
                        result = num2;
                        break;
                }
            }
            if (((min_float <= result) && (result <= max_float) )||(result==0))     // 計算結果が範囲内
            {
                label1.Text = result.ToString();        
                buttun_push = null;                     
                Button btn = (Button)sender;          
                Operator = btn.Text;                    
                syousuu = false;                        
                if (Operator == "＝")                 // operatorが「＝」であるとき
                {
                    Operator = null;                  // 演算子をクリア
                }
            }
            if( ((result >= max_float) || (result <= min_float) )&&(result!=0))     // floatの範囲外(オーバーフロー処理)
            { 
                label1.Text = "エラー";        
                buttun_push = null;
                Operator = null;                
                result = 0;                     
                syousuu = false;                
            }
        }

        /*******CA,CEが押されたとき *******/
        private void button_clr_Click(object sender, EventArgs e) 
        {
            Button btn = (Button)sender;        // CAかCEを判断する
            clr = btn.Text;
            if (clr == "CA")
            {
                Operator = null;                // 演算子クリア
                result = 0;                     // 結果クリア
            }
            buttun_push = null;                 
            syousuu = false;                    
            label1.Text ="0";                   
        }
    }
}

