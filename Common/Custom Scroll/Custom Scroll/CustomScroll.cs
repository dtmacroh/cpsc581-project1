using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace PhidgetsControlLibrary {
	public partial class CustomScroll : UserControl {

		double minValue = -1;
		double maxValue = -1;
		double logValue = -1;
		double logBase = 0.00000005; //controls the slope of the log funciton.  very aggressive here so that the bulk of the scale is usable and only the last half jumps quickly.

		public CustomScroll() {
			InitializeComponent();
			Unit = "";
		}


		public event EventHandler LogScroll {
			add { slider.Scroll += value; }
			remove { slider.Scroll -= value; }
		}

		public event MouseEventHandler LogMouseUp {
			add { slider.MouseUp += value; }
			remove { slider.MouseUp -= value; }
		}

		#region Properties


		public double Minimum {
			get {
				return this.minValue;
			}
			set {
				slider.Minimum = 0;
				this.minValue = value;
			}
		}

		public double Maximum {
			get {
				return this.maxValue;
			}
			set { //slider should have 100 positions that range between minValue and maxValue
				slider.Maximum = 100;
				this.maxValue = value;
			}
		}

		public bool isIntValue { get; set; }

		public string Unit { get; set; }

		public string labelTxt {
			get {
				return trkName.Text;
			}
			set {
				trkName.Text = value;
			}
		}

		public string minTxt {
			get {
				return minLbl.Text;
			}
			set {
				minLbl.Text = value;
			}
		}

		public string maxTxt {
			get {
				return maxLbl.Text;
			}
			set {
				maxLbl.Text = value;
			}
		}

		public string trkText {
			get {
				return trkTxt.Text;
			}
			set {
				trkTxt.Text = value;
			}
		}

		public double Value {
			get {
				return this.logValue;
			}
			set {
				logValue = value;

				//testing inverse of the log function instead of magic numbers
				if (minValue != maxValue) {
					slider.Value = (int)((slider.Maximum * (Math.Pow(logBase, -(logValue - minValue) / (minValue - maxValue)) - 1)) / (logBase - 1));

					updateValue();
				}
			}
		}

		private void updateValue() {
			double newValue = 0;

			if (minValue != maxValue && logValue >= 0) {
				newValue = Math.Log(((slider.Value) * (logBase - 1)) / slider.Maximum + 1, logBase) * (maxValue - minValue) + minValue;
				if (!isIntValue) {
					if (maxValue <= 5)
						trkTxt.Text = newValue.ToString("F3") + Unit;
					else
						trkTxt.Text = newValue.ToString("F2") + Unit;
				} else {
					if (maxValue <= 5)
						trkTxt.Text = newValue.ToString("F0") + Unit;
					else
						trkTxt.Text = newValue.ToString("F0") + Unit;
				}
				logValue = newValue;
			}
		}

		#endregion

		private void slider_Scroll(object sender, EventArgs e) {
			updateValue();
		}
	}
}
