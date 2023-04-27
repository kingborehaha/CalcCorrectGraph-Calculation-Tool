using System;
using System.Windows.Forms;

namespace CalcCorrectGraph_Calculation_Tool
{

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs ee)
        {

            Activated += Calculate; //calculate on initial form load
            
            //calculate output if any value is changed
            InputValButton.ValueChanged += Calculate;
            InputStageMaxVal0.ValueChanged += Calculate;
            InputStageMaxVal1.ValueChanged += Calculate;
            InputStageMaxVal2.ValueChanged += Calculate;
            InputStageMaxVal3.ValueChanged += Calculate;
            InputStageMaxVal4.ValueChanged += Calculate;
            inputGrowVal0.ValueChanged += Calculate;
            inputGrowVal1.ValueChanged += Calculate;
            inputGrowVal2.ValueChanged += Calculate;
            inputGrowVal3.ValueChanged += Calculate;
            inputGrowVal4.ValueChanged += Calculate;
            inputMultVal0.ValueChanged += Calculate;
            inputMultVal1.ValueChanged += Calculate;
            inputMultVal2.ValueChanged += Calculate;
            inputMultVal3.ValueChanged += Calculate;
            inputMultVal4.ValueChanged += Calculate;
            initCost.ValueChanged += Calculate;
            initOffset.ValueChanged += Calculate;
            costIncrease.ValueChanged += Calculate;
            increaseInterval.ValueChanged += Calculate;
            
            //highlight entire input if tabbed into
            InputValButton.GotFocus += (o, e) => InputValButton.Select(0, InputValButton.Text.Length);
            InputStageMaxVal0.GotFocus += (o, e) => InputStageMaxVal0.Select(0, InputStageMaxVal0.Text.Length);
            InputStageMaxVal1.GotFocus += (o, e) => InputStageMaxVal1.Select(0, InputStageMaxVal1.Text.Length);
            InputStageMaxVal2.GotFocus += (o, e) => InputStageMaxVal2.Select(0, InputStageMaxVal2.Text.Length);
            InputStageMaxVal3.GotFocus += (o, e) => InputStageMaxVal3.Select(0, InputStageMaxVal3.Text.Length);
            InputStageMaxVal4.GotFocus += (o, e) => InputStageMaxVal4.Select(0, InputStageMaxVal4.Text.Length);
            inputGrowVal0.GotFocus += (o, e) => inputGrowVal0.Select(0, inputGrowVal0.Text.Length);
            inputGrowVal1.GotFocus += (o, e) => inputGrowVal1.Select(0, inputGrowVal1.Text.Length);
            inputGrowVal2.GotFocus += (o, e) => inputGrowVal2.Select(0, inputGrowVal2.Text.Length);
            inputGrowVal3.GotFocus += (o, e) => inputGrowVal3.Select(0, inputGrowVal3.Text.Length);
            inputGrowVal4.GotFocus += (o, e) => inputGrowVal4.Select(0, inputGrowVal4.Text.Length);
            inputMultVal0.GotFocus += (o, e) => inputMultVal0.Select(0, inputMultVal0.Text.Length);
            inputMultVal1.GotFocus += (o, e) => inputMultVal1.Select(0, inputMultVal1.Text.Length);
            inputMultVal2.GotFocus += (o, e) => inputMultVal2.Select(0, inputMultVal2.Text.Length);
            inputMultVal3.GotFocus += (o, e) => inputMultVal3.Select(0, inputMultVal3.Text.Length);
            inputMultVal4.GotFocus += (o, e) => inputMultVal4.Select(0, inputMultVal4.Text.Length);
            initCost.GotFocus += (o, e) => initCost.Select(0, initCost.Text.Length);
            initOffset.GotFocus += (o, e) => initOffset.Select(0, initOffset.Text.Length);
            costIncrease.GotFocus += (o, e) => costIncrease.Select(0, costIncrease.Text.Length);
            increaseInterval.GotFocus += (o, e) => increaseInterval.Select(0, increaseInterval.Text.Length);

        }

        public void CalculateOutput(double stageMin, double stageMax, double valMin, double valMax, double inputVal, double multValMin, double multValMax )
        {
            double output;
            //v3 (perfect. no mults)
            /*
            decimal inputRatio = (inputVal - stageMin) / (stageMax - stageMin);
            output = valMin + ((valMax - valMin) * inputRatio); //standard output
            */

            //v11
            double inputRatio = (inputVal - stageMin) / (stageMax - stageMin);
            double growthVal;

            //calculate differently depending on if mult val is negative or positive
            if (multValMin > 0)
                growthVal = Math.Pow(inputRatio, multValMin);
            else
                growthVal = 1 - Math.Pow(1 - inputRatio, Math.Abs(multValMin));

            //output = Math.Floor(valMin + ((valMax - valMin) * growthVal)); //standard output (floored for integers like HP)
            output = valMin + ((valMax - valMin) * growthVal); //standard output (scaling calculations use decimals)

            output = Math.Round(output * 1000) / 1000; //clip off most of the decimals because it's annoying
            //System.Diagnostics.Debug.WriteLine("raw output: " + output);

            OutputTextBox.Text = output.ToString("G"); //print final value (G = remove empty decimals)
            return;
        }

        public void CalculateLevelCost(double inpVal, double iC, double iO, double cI, double iI)
        {

            double outputCost;
            double funcMod = ((inpVal + 81) - iI) * cI;
            if (funcMod < 0)
            {
                funcMod = 0;
            }

            outputCost = ((funcMod + iC) * (Math.Pow((inpVal + 81), 2))) + iO;

            outputCost = Math.Floor(outputCost);

            levelCostTextbox.Text = outputCost.ToString();

        }


        private void Calculate(object sender, EventArgs e)
        {
            double stageVal0 = Convert.ToDouble(InputStageMaxVal0.Value);
            double stageVal1 = Convert.ToDouble(InputStageMaxVal1.Value);
            double stageVal2 = Convert.ToDouble(InputStageMaxVal2.Value);
            double stageVal3 = Convert.ToDouble(InputStageMaxVal3.Value);
            double stageVal4 = Convert.ToDouble(InputStageMaxVal4.Value);
            double growVal0 = Convert.ToDouble(inputGrowVal0.Value);
            double growVal1 = Convert.ToDouble(inputGrowVal1.Value);
            double growVal2 = Convert.ToDouble(inputGrowVal2.Value);
            double growVal3 = Convert.ToDouble(inputGrowVal3.Value);
            double growVal4 = Convert.ToDouble(inputGrowVal4.Value);
            double multVal0 = Convert.ToDouble(inputMultVal0.Value);
            double multVal1 = Convert.ToDouble(inputMultVal1.Value);
            double multVal2 = Convert.ToDouble(inputMultVal2.Value);
            double multVal3 = Convert.ToDouble(inputMultVal3.Value);
            double multVal4 = Convert.ToDouble(inputMultVal4.Value);
            double inputVal = Convert.ToDouble(InputValButton.Value);
            double initialCost = Convert.ToDouble(initCost.Value);
            double initialOffset = Convert.ToDouble(initOffset.Value);
            double increaseCost = Convert.ToDouble(costIncrease.Value);
            double intervalIncrease = Convert.ToDouble(increaseInterval.Value);


            //error check. if stage max and min are the same then it will divide by zero
            if (stageVal0 >= stageVal1 || stageVal1 >= stageVal2 || stageVal2 >= stageVal3 || stageVal3 >= stageVal4){
                //error: stage values not valid
                OutputTextBox.Text = "ERR: !ASCEND";
            }
            else if (inputVal < stageVal0){
                //error: input is less than stage 0
                OutputTextBox.Text = "ERR: <STAGE0";
            }
            else if (inputVal <= stageVal1){
                //stage 0-1
                CalculateOutput(stageVal0, stageVal1, growVal0, growVal1, inputVal, multVal0, multVal1);
            }
            else if (inputVal <= stageVal2){
                //stage 1-2
                CalculateOutput(stageVal1, stageVal2, growVal1, growVal2, inputVal, multVal1, multVal2);
            }
            else if (inputVal <= stageVal3){
                //stage 2-3
                CalculateOutput(stageVal2, stageVal3, growVal2, growVal3, inputVal, multVal2, multVal3);
            }
            else if (inputVal <= stageVal4){
                //stage 3-4 (and edge case beyond)
                CalculateOutput(stageVal3, stageVal4, growVal3, growVal4, inputVal, multVal3, multVal4);
            }
            else{
                //error: input value greater than stage 4
                OutputTextBox.Text = "ERR: >STAGE4";
            }
            if (inputVal > 0 && inputVal % 1 == 0)
            {
                CalculateLevelCost(inputVal, initialCost, initialOffset, increaseCost, intervalIncrease);
            }
            else
            {
                levelCostTextbox.Text = "ERR: <0; =/= integer";
            }

        }

        private void ButtonDebugSet_Click(object sender, EventArgs e)
        {
            //for debug button. inputs values into each field
            InputStageMaxVal0.Value = 1M;
            InputStageMaxVal1.Value = 15M;
            InputStageMaxVal2.Value = 30M;
            InputStageMaxVal3.Value = 40M;
            InputStageMaxVal4.Value = 99M;
            inputGrowVal0.Value = 400M;
            inputGrowVal1.Value = 682M;
            inputGrowVal2.Value = 1000M;
            inputGrowVal3.Value = 1200M;
            inputGrowVal4.Value = 1700M;
            inputMultVal0.Value = 1.1M;
            inputMultVal1.Value = 1.2M;
            inputMultVal2.Value = -1.2M;
            inputMultVal3.Value = -1.1M;
            inputMultVal4.Value = 1M;
            InputValButton.Value = 30M;
        }

    }

    public class HideDecimalNumericUpDown : NumericUpDown
    {
        protected override void UpdateEditText()
        {
            Text = Value.ToString("G"); //hide unused decimals
        }
    }
}
