﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculator
{
    public partial class FormMain : Form
    {
        public enum SymbolType
        {
            Number,
            Operator,
            SpecialOp,
            DecimalPoint,
            backspace,
            PlusMinusSign,
            cancellAll,
            cancellEntry,
            undefined
        }
        public struct BtnStruct
        {
            public char Content;
            public SymbolType type;
            public bool IsBold;
            public bool IsNumber;
            public BtnStruct(char c, SymbolType t = SymbolType.undefined, bool b = false, bool n = false)
            {
                this.Content = c;
                this.type = t;
                this.IsBold = b;
                this.IsNumber = n;
            }
        }
        float lblResultBaseFontSize;
        const int lblResultWidthMargin = 24;
        const int lblResultMaxDigit = 25;
        char lastOperator = ' ';
        decimal op1, op2, result;
        BtnStruct lastButtonClicked;

        private BtnStruct[,] buttons =
        {
           { new BtnStruct('%', SymbolType.SpecialOp), new BtnStruct('\u0152',SymbolType.cancellEntry), new BtnStruct('C',SymbolType.cancellAll), new BtnStruct('\u232B',SymbolType.backspace) },
            { new BtnStruct('\u215F',SymbolType.SpecialOp), new BtnStruct('\u00B2', SymbolType.SpecialOp), new BtnStruct('\u221A', SymbolType.SpecialOp), new BtnStruct('\u00F7',SymbolType.Operator )},
            { new BtnStruct('7',SymbolType.Number,true,true), new BtnStruct('8',SymbolType.Number,true,true), new BtnStruct('9',SymbolType.Number,true,true), new BtnStruct('\u00D7',SymbolType.Operator) },
            { new BtnStruct('4',SymbolType.Number,true,true), new BtnStruct('5',SymbolType.Number,true,true), new BtnStruct('6',SymbolType.Number,true,true), new BtnStruct('-',SymbolType.Operator) },
            { new BtnStruct('1',SymbolType.Number,true,true), new BtnStruct('2',SymbolType.Number,true,true), new BtnStruct('3',SymbolType.Number,true,true), new BtnStruct('+',SymbolType.Operator) },
            { new BtnStruct('\u00B1',SymbolType.PlusMinusSign,true), new BtnStruct('0',SymbolType.Number,true,true), new BtnStruct(',',SymbolType.DecimalPoint,true), new BtnStruct('=',SymbolType.Operator) },
        };

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            MakeButtons(buttons.GetLength(0), buttons.GetLength(1));
            lblResultBaseFontSize = lbl_result.Font.Size;
            lblCrono.Text = "";
        }

        private void MakeButtons(int rows, int cols)
        {
            int btnWidth = 80;
            int btnHeight = 60;
            int posX = 0;
            int posY = 116;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Button myButton = new Button();
                    FontStyle fs = buttons[i, j].IsBold ? FontStyle.Bold : FontStyle.Regular;
                    myButton.Font = new Font("Segoe UI", 16, fs);
                    myButton.BackColor = buttons[i, j].IsBold ? Color.White : Color.Transparent;
                    myButton.Text = buttons[i, j].Content.ToString();
                    myButton.Width = btnWidth;
                    myButton.Height = btnHeight;
                    myButton.Top = posY;
                    myButton.Left = posX;
                    myButton.Tag = buttons[i, j];
                    myButton.Click += Button_Click;
                    this.Controls.Add(myButton);
                    posX += myButton.Width;
                }
                posX = 0;
                posY += btnHeight;
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            BtnStruct clickedButtonStruct = (BtnStruct)clickedButton.Tag;
         

            switch (clickedButtonStruct.type)
            {
                case SymbolType.Number:
                    if (lbl_result.Text == "0" || lastButtonClicked.type == SymbolType.Operator || lastButtonClicked.type == SymbolType.SpecialOp)
                    {
                        lbl_result.Text = "";
                    }
                    lbl_result.Text += clickedButton.Text;
                    
                    break;

                case SymbolType.SpecialOp:
                    
                    specialOperatorManage(clickedButtonStruct);
                    break;
                case SymbolType.Operator:               
                    if (lastButtonClicked.type == SymbolType.Operator && clickedButtonStruct.Content != '=')
                    {
                        lastOperator = clickedButtonStruct.Content;
                        lblCrono.Text = " ";
                        lblCrono.Text = lbl_result.Text + " " + clickedButton.Text  +" ";

                    }
                    else
                    {
                        ManageOperator(clickedButtonStruct);

                        if (clickedButtonStruct.Content != '=')
                        {

                            lblCrono.Text = " ";
                            lblCrono.Text = lbl_result.Text + " " + clickedButton.Text + " ";
                        }
                            
                        
                            
                    }

                    break;
                

                case SymbolType.DecimalPoint:
                    if (lbl_result.Text.IndexOf(",") == -1)
                    {
                        lblCrono.Text = "";
                        lbl_result.Text += clickedButton.Text;
                    }
                    break;
                case SymbolType.PlusMinusSign:
                    //lblCrono.Text += " " + clickedButton.Text;
                    if (lbl_result.Text != "0")
                    {
                        if (lbl_result.Text.IndexOf("-") == -1)
                        {
                            lbl_result.Text = "-" + lbl_result.Text;
                        }
                        else
                        {
                            lbl_result.Text = lbl_result.Text.Substring(1);
                        }
                        if(lastButtonClicked.type== SymbolType.Operator)
                        {
                            op1 = -op2;
                        }
                    }
                    break;
                case SymbolType.backspace:
                    if (lastButtonClicked.type != SymbolType.Operator && lastButtonClicked.type != SymbolType.SpecialOp)
                    {
                        lbl_result.Text = lbl_result.Text.Substring(0, lbl_result.Text.Length - 1);
                        if (lbl_result.Text.Length == 0 || lbl_result.Text == "-0" || lbl_result.Text == "-")
                        {
                            lbl_result.Text = "0";
                        }
                    }

                    break;
                case SymbolType.cancellAll:
                    clear();

                    break;
                case SymbolType.cancellEntry:
                    if (lastButtonClicked.Content == '=')
                    {
                        clear();
                    }
                    else
                    {
                        lbl_result.Text = "0";
                      //  lblCrono.Text = "";
                    }
                    break;
                case SymbolType.undefined:
                    break;
                default:
                    break;
            }
            if (clickedButtonStruct.type != SymbolType.backspace || clickedButtonStruct.type != SymbolType.cancellAll)
                lastButtonClicked = clickedButtonStruct;

        }

        

        private void clear()
        {
            op1 = 0;
            op2 = 0;
            result = 0;
            lastOperator = ' ';
            lbl_result.Text = "0";
            if(lastButtonClicked.Content != '\u0152')
            {
                lblCrono.Text = "";
            }
            
        }

        private void specialOperatorManage(BtnStruct clickedButtonStruct)
        {
            op2 = decimal.Parse(lbl_result.Text);
            switch (clickedButtonStruct.Content)
            {
                case '\u215F': // 1/x
                    if (lblCrono.Text == "")
                    {
                        lblCrono.Text += "1/( " + lbl_result.Text + " )";

                    }
                    else
                    {
                        lblCrono.Text = "1/( " + lblCrono.Text + " )";
                    }

                    result = 1 / op2;

                    break;
                case '%':
                    result=op1*op2/100;
                    lblCrono.Text += " "+result;
                    if (lblCrono.Text==" 0,000"){
                        result= 0;
                        lblCrono.Text = "0";
                    }
                    break;
                case '\u00B2': //x alla seconda
                     if (lblCrono.Text == "")
                    {
                        lblCrono.Text += "sqr( " + lbl_result.Text + " )";

                    }
                    else if (lastButtonClicked.type == clickedButtonStruct.type)
                    {
                        lblCrono.Text = "sqr( " + lblCrono.Text + " )";
                    }
                    else if(lbl_result.Text != "")
                    {
                        lblCrono.Text = "sqr( " + lbl_result.Text + " )";
                    }else
                    {
                        lblCrono.Text = "sqr( " + lblCrono.Text + " )";
                    }
                    
                    result = op2 * op2;
                    break;
                case '\u221A': //radice
                    if (lblCrono.Text == "")
                    {
                    lblCrono.Text += '\u221A'+"( "  + lbl_result.Text + " )";

                    }
                    else if (lastButtonClicked.type == clickedButtonStruct.type)
                    {
                        lblCrono.Text = '\u221A' + "( " + lblCrono.Text + " )";
                    }
                    else
                    {
                        if (lastButtonClicked.Content == '=')
                        {
                            lblCrono.Text = '\u221A' + "( " + lbl_result.Text + " )";
                        }
                        else
                        {
                        lblCrono.Text += '\u221A' + "( " + lbl_result.Text + " )";
                        }
                    }
                    result = (decimal)Math.Sqrt((double)op2);
                    break;
                default:
                    break;      
            }
            lbl_result.Text = result.ToString();
        }

        private void ManageOperator(BtnStruct clickedButtonStruct)
        {
            
            if (lastOperator == ' ' )
            {
                op1 = decimal.Parse(lbl_result.Text);
                if (clickedButtonStruct.Content != '=')
                {
                    lastOperator = clickedButtonStruct.Content;
                }
                else
                {
                    lblCrono.Text = lbl_result.Text + " " + "=";
                }
            }
            else
            {
                if (lastButtonClicked.Content != '=')
                    op2 = decimal.Parse(lbl_result.Text);
                switch (lastOperator)
                {
                    case '+':
                        
                        result = op1 + op2;

                        break;

                    case '-':
                        result = op1 - op2;
                        break;

                    case '\u00D7':
                        result = op1 * op2;
                        break;

                    case '\u00F7':
                        result = op1 / op2;
                        break;

                    default:
                        break;
                }
                lblCrono.Text = op1 + " " + lastOperator + " "+op2+" " + "=";
                op1 = result;
                if (clickedButtonStruct.Content != '=')
                {
                    lastOperator = clickedButtonStruct.Content;
                    if (lastButtonClicked.Content == '=') op2 = 0;
                }
                lbl_result.Text = result.ToString();
            }
        }

        private void lbl_result_TextChanged(object sender, EventArgs e)
        {
            if (lbl_result.Text == "-")
            {
                lbl_result.Text = "0";
                return;
            }
            if (lbl_result.Text.Length > 0)
            {
                decimal num = decimal.Parse(lbl_result.Text); string stOut = "";
                NumberFormatInfo nfi = new CultureInfo("it-IT", false).NumberFormat;
                int decimalSeparatorPosition = lbl_result.Text.IndexOf(",");
                nfi.NumberDecimalDigits = decimalSeparatorPosition == -1 ? 0
                  : lbl_result.Text.Length - decimalSeparatorPosition - 1;
                stOut = num.ToString("N", nfi);
                if (lbl_result.Text.IndexOf(",") == lbl_result.Text.Length - 1) stOut += ",";
                lbl_result.Text = stOut;


            }

            if (lbl_result.Text.Length > lblResultMaxDigit)
                lbl_result.Text = lbl_result.Text.Substring(0, lblResultMaxDigit);

            int textWidth = TextRenderer.MeasureText(lbl_result.Text, lbl_result.Font).Width;
            float newSize = lbl_result.Font.Size * (((float)lbl_result.Size.Width - lblResultWidthMargin) / textWidth);
            if (newSize > lblResultBaseFontSize)
            {
                newSize = lblResultBaseFontSize;
            }
            lbl_result.Font = new Font("Segoe UI", newSize, FontStyle.Regular);


        }
    }
}
