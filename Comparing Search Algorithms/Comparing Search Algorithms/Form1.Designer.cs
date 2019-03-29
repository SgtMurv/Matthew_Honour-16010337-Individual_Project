namespace Comparing_Search_Algorithms
{
    partial class frmComparingSearchAlgorithms
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmComparingSearchAlgorithms));
            this.grpbxGenerateNetwork = new System.Windows.Forms.GroupBox();
            this.txtNoOfNodes = new System.Windows.Forms.TextBox();
            this.lblNumberOfNodes = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.grpbxSearchForOptimalRoute = new System.Windows.Forms.GroupBox();
            this.lblStateOfSearch = new System.Windows.Forms.Label();
            this.btnBeginSearchAlgorithm = new System.Windows.Forms.Button();
            this.lblSelectHeuristic = new System.Windows.Forms.Label();
            this.cbxSearchingAlgorithms = new System.Windows.Forms.ComboBox();
            this.btnCalculateAverage = new System.Windows.Forms.Button();
            this.grpbxPerformanceDisplay = new System.Windows.Forms.GroupBox();
            this.lblSelectGraphToView = new System.Windows.Forms.Label();
            this.cbxGraphToView = new System.Windows.Forms.ComboBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnEditSearchOption = new System.Windows.Forms.Button();
            this.txtSimulatedAnnealing = new System.Windows.Forms.TextBox();
            this.lblSimulatedAnnealing = new System.Windows.Forms.Label();
            this.lblHillClimbing = new System.Windows.Forms.Label();
            this.txtInitalDistance = new System.Windows.Forms.TextBox();
            this.txtHillClimbing = new System.Windows.Forms.TextBox();
            this.lblOptimiseTitle = new System.Windows.Forms.Label();
            this.lblInitialDistance = new System.Windows.Forms.Label();
            this.chGridofCities = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.txtDisplaying = new System.Windows.Forms.Label();
            this.lblCurrentGraph = new System.Windows.Forms.Label();
            this.showGridCheckbox = new System.Windows.Forms.CheckBox();
            this.grpbxGenerateNetwork.SuspendLayout();
            this.grpbxSearchForOptimalRoute.SuspendLayout();
            this.grpbxPerformanceDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chGridofCities)).BeginInit();
            this.SuspendLayout();
            // 
            // grpbxGenerateNetwork
            // 
            this.grpbxGenerateNetwork.Controls.Add(this.txtNoOfNodes);
            this.grpbxGenerateNetwork.Controls.Add(this.lblNumberOfNodes);
            this.grpbxGenerateNetwork.Controls.Add(this.btnGenerate);
            this.grpbxGenerateNetwork.Location = new System.Drawing.Point(8, 544);
            this.grpbxGenerateNetwork.Name = "grpbxGenerateNetwork";
            this.grpbxGenerateNetwork.Size = new System.Drawing.Size(203, 129);
            this.grpbxGenerateNetwork.TabIndex = 0;
            this.grpbxGenerateNetwork.TabStop = false;
            this.grpbxGenerateNetwork.Text = "Step 1: Generate Network";
            // 
            // txtNoOfNodes
            // 
            this.txtNoOfNodes.Location = new System.Drawing.Point(9, 32);
            this.txtNoOfNodes.Name = "txtNoOfNodes";
            this.txtNoOfNodes.Size = new System.Drawing.Size(185, 20);
            this.txtNoOfNodes.TabIndex = 2;
            // 
            // lblNumberOfNodes
            // 
            this.lblNumberOfNodes.AutoSize = true;
            this.lblNumberOfNodes.Location = new System.Drawing.Point(6, 16);
            this.lblNumberOfNodes.Name = "lblNumberOfNodes";
            this.lblNumberOfNodes.Size = new System.Drawing.Size(93, 13);
            this.lblNumberOfNodes.TabIndex = 1;
            this.lblNumberOfNodes.Text = "Number of Nodes:";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(6, 78);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(188, 43);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "Generate graph";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // grpbxSearchForOptimalRoute
            // 
            this.grpbxSearchForOptimalRoute.Controls.Add(this.lblStateOfSearch);
            this.grpbxSearchForOptimalRoute.Controls.Add(this.btnBeginSearchAlgorithm);
            this.grpbxSearchForOptimalRoute.Controls.Add(this.lblSelectHeuristic);
            this.grpbxSearchForOptimalRoute.Controls.Add(this.cbxSearchingAlgorithms);
            this.grpbxSearchForOptimalRoute.Enabled = false;
            this.grpbxSearchForOptimalRoute.Location = new System.Drawing.Point(217, 544);
            this.grpbxSearchForOptimalRoute.Name = "grpbxSearchForOptimalRoute";
            this.grpbxSearchForOptimalRoute.Size = new System.Drawing.Size(249, 129);
            this.grpbxSearchForOptimalRoute.TabIndex = 0;
            this.grpbxSearchForOptimalRoute.TabStop = false;
            this.grpbxSearchForOptimalRoute.Text = "Step 2: Select Optimisation Method";
            // 
            // lblStateOfSearch
            // 
            this.lblStateOfSearch.AutoSize = true;
            this.lblStateOfSearch.Location = new System.Drawing.Point(6, 58);
            this.lblStateOfSearch.Name = "lblStateOfSearch";
            this.lblStateOfSearch.Size = new System.Drawing.Size(0, 13);
            this.lblStateOfSearch.TabIndex = 5;
            // 
            // btnBeginSearchAlgorithm
            // 
            this.btnBeginSearchAlgorithm.Location = new System.Drawing.Point(6, 77);
            this.btnBeginSearchAlgorithm.Name = "btnBeginSearchAlgorithm";
            this.btnBeginSearchAlgorithm.Size = new System.Drawing.Size(237, 43);
            this.btnBeginSearchAlgorithm.TabIndex = 4;
            this.btnBeginSearchAlgorithm.Text = "Optimise Route";
            this.btnBeginSearchAlgorithm.UseVisualStyleBackColor = true;
            this.btnBeginSearchAlgorithm.Click += new System.EventHandler(this.btnBeginSearchAlgorithm_Click);
            // 
            // lblSelectHeuristic
            // 
            this.lblSelectHeuristic.AutoSize = true;
            this.lblSelectHeuristic.Location = new System.Drawing.Point(6, 16);
            this.lblSelectHeuristic.Name = "lblSelectHeuristic";
            this.lblSelectHeuristic.Size = new System.Drawing.Size(194, 13);
            this.lblSelectHeuristic.TabIndex = 1;
            this.lblSelectHeuristic.Text = "Please select an Optimisation Algorithm:";
            // 
            // cbxSearchingAlgorithms
            // 
            this.cbxSearchingAlgorithms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSearchingAlgorithms.FormattingEnabled = true;
            this.cbxSearchingAlgorithms.Items.AddRange(new object[] {
            "Hill Climbing",
            "Simulated Annealing",
            "Run all Algorithms"});
            this.cbxSearchingAlgorithms.Location = new System.Drawing.Point(6, 32);
            this.cbxSearchingAlgorithms.Name = "cbxSearchingAlgorithms";
            this.cbxSearchingAlgorithms.Size = new System.Drawing.Size(237, 21);
            this.cbxSearchingAlgorithms.TabIndex = 0;
            // 
            // btnCalculateAverage
            // 
            this.btnCalculateAverage.Enabled = false;
            this.btnCalculateAverage.Location = new System.Drawing.Point(764, 16);
            this.btnCalculateAverage.Name = "btnCalculateAverage";
            this.btnCalculateAverage.Size = new System.Drawing.Size(237, 46);
            this.btnCalculateAverage.TabIndex = 15;
            this.btnCalculateAverage.Text = "Calculate Average Percentage Decrease";
            this.btnCalculateAverage.UseVisualStyleBackColor = true;
            this.btnCalculateAverage.Click += new System.EventHandler(this.btnCalculateAverage_Click);
            // 
            // grpbxPerformanceDisplay
            // 
            this.grpbxPerformanceDisplay.Controls.Add(this.lblSelectGraphToView);
            this.grpbxPerformanceDisplay.Controls.Add(this.cbxGraphToView);
            this.grpbxPerformanceDisplay.Controls.Add(this.btnReset);
            this.grpbxPerformanceDisplay.Controls.Add(this.btnEditSearchOption);
            this.grpbxPerformanceDisplay.Controls.Add(this.txtSimulatedAnnealing);
            this.grpbxPerformanceDisplay.Controls.Add(this.lblSimulatedAnnealing);
            this.grpbxPerformanceDisplay.Controls.Add(this.lblHillClimbing);
            this.grpbxPerformanceDisplay.Controls.Add(this.txtInitalDistance);
            this.grpbxPerformanceDisplay.Controls.Add(this.txtHillClimbing);
            this.grpbxPerformanceDisplay.Controls.Add(this.lblOptimiseTitle);
            this.grpbxPerformanceDisplay.Controls.Add(this.lblInitialDistance);
            this.grpbxPerformanceDisplay.Enabled = false;
            this.grpbxPerformanceDisplay.Location = new System.Drawing.Point(472, 544);
            this.grpbxPerformanceDisplay.Name = "grpbxPerformanceDisplay";
            this.grpbxPerformanceDisplay.Size = new System.Drawing.Size(535, 129);
            this.grpbxPerformanceDisplay.TabIndex = 0;
            this.grpbxPerformanceDisplay.TabStop = false;
            this.grpbxPerformanceDisplay.Text = "Step 3: View Performance";
            // 
            // lblSelectGraphToView
            // 
            this.lblSelectGraphToView.AutoSize = true;
            this.lblSelectGraphToView.Location = new System.Drawing.Point(6, 16);
            this.lblSelectGraphToView.Name = "lblSelectGraphToView";
            this.lblSelectGraphToView.Size = new System.Drawing.Size(107, 13);
            this.lblSelectGraphToView.TabIndex = 14;
            this.lblSelectGraphToView.Text = "Select graph to view:";
            // 
            // cbxGraphToView
            // 
            this.cbxGraphToView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxGraphToView.FormattingEnabled = true;
            this.cbxGraphToView.Location = new System.Drawing.Point(6, 31);
            this.cbxGraphToView.Name = "cbxGraphToView";
            this.cbxGraphToView.Size = new System.Drawing.Size(183, 21);
            this.cbxGraphToView.TabIndex = 13;
            this.cbxGraphToView.SelectedIndexChanged += new System.EventHandler(this.cbxGraphToView_SelectedIndexChanged);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(6, 58);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(185, 28);
            this.btnReset.TabIndex = 12;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnEditSearchOption
            // 
            this.btnEditSearchOption.Location = new System.Drawing.Point(6, 92);
            this.btnEditSearchOption.Name = "btnEditSearchOption";
            this.btnEditSearchOption.Size = new System.Drawing.Size(185, 28);
            this.btnEditSearchOption.TabIndex = 11;
            this.btnEditSearchOption.Text = "Change Optimisation Method";
            this.btnEditSearchOption.UseVisualStyleBackColor = true;
            this.btnEditSearchOption.Click += new System.EventHandler(this.btnEditSearchOption_Click);
            // 
            // txtSimulatedAnnealing
            // 
            this.txtSimulatedAnnealing.Location = new System.Drawing.Point(344, 94);
            this.txtSimulatedAnnealing.Name = "txtSimulatedAnnealing";
            this.txtSimulatedAnnealing.Size = new System.Drawing.Size(185, 20);
            this.txtSimulatedAnnealing.TabIndex = 7;
            // 
            // lblSimulatedAnnealing
            // 
            this.lblSimulatedAnnealing.AutoSize = true;
            this.lblSimulatedAnnealing.Location = new System.Drawing.Point(197, 97);
            this.lblSimulatedAnnealing.Name = "lblSimulatedAnnealing";
            this.lblSimulatedAnnealing.Size = new System.Drawing.Size(115, 13);
            this.lblSimulatedAnnealing.TabIndex = 6;
            this.lblSimulatedAnnealing.Text = "Simulated Annealing = ";
            // 
            // lblHillClimbing
            // 
            this.lblHillClimbing.AutoSize = true;
            this.lblHillClimbing.Location = new System.Drawing.Point(197, 71);
            this.lblHillClimbing.Name = "lblHillClimbing";
            this.lblHillClimbing.Size = new System.Drawing.Size(75, 13);
            this.lblHillClimbing.TabIndex = 5;
            this.lblHillClimbing.Text = "Hill Climbing = ";
            // 
            // txtInitalDistance
            // 
            this.txtInitalDistance.Location = new System.Drawing.Point(344, 25);
            this.txtInitalDistance.Name = "txtInitalDistance";
            this.txtInitalDistance.Size = new System.Drawing.Size(185, 20);
            this.txtInitalDistance.TabIndex = 3;
            // 
            // txtHillClimbing
            // 
            this.txtHillClimbing.Location = new System.Drawing.Point(344, 68);
            this.txtHillClimbing.Name = "txtHillClimbing";
            this.txtHillClimbing.Size = new System.Drawing.Size(185, 20);
            this.txtHillClimbing.TabIndex = 2;
            // 
            // lblOptimiseTitle
            // 
            this.lblOptimiseTitle.AutoSize = true;
            this.lblOptimiseTitle.Location = new System.Drawing.Point(197, 48);
            this.lblOptimiseTitle.Name = "lblOptimiseTitle";
            this.lblOptimiseTitle.Size = new System.Drawing.Size(175, 13);
            this.lblOptimiseTitle.TabIndex = 1;
            this.lblOptimiseTitle.Text = "Distance of Optimised Route Using:";
            // 
            // lblInitialDistance
            // 
            this.lblInitialDistance.AutoSize = true;
            this.lblInitialDistance.Location = new System.Drawing.Point(197, 31);
            this.lblInitialDistance.Name = "lblInitialDistance";
            this.lblInitialDistance.Size = new System.Drawing.Size(141, 13);
            this.lblInitialDistance.TabIndex = 0;
            this.lblInitialDistance.Text = "Distance of Random Path = ";
            // 
            // chGridofCities
            // 
            chartArea1.Name = "ChartArea1";
            this.chGridofCities.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chGridofCities.Legends.Add(legend1);
            this.chGridofCities.Location = new System.Drawing.Point(8, 87);
            this.chGridofCities.Name = "chGridofCities";
            this.chGridofCities.Size = new System.Drawing.Size(999, 451);
            this.chGridofCities.TabIndex = 0;
            this.chGridofCities.Text = "Grid of Cities";
            // 
            // txtDisplaying
            // 
            this.txtDisplaying.AutoSize = true;
            this.txtDisplaying.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDisplaying.Location = new System.Drawing.Point(7, 9);
            this.txtDisplaying.Name = "txtDisplaying";
            this.txtDisplaying.Size = new System.Drawing.Size(314, 42);
            this.txtDisplaying.TabIndex = 1;
            this.txtDisplaying.Text = "Current Graph = ";
            // 
            // lblCurrentGraph
            // 
            this.lblCurrentGraph.AutoSize = true;
            this.lblCurrentGraph.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentGraph.Location = new System.Drawing.Point(302, 9);
            this.lblCurrentGraph.Name = "lblCurrentGraph";
            this.lblCurrentGraph.Size = new System.Drawing.Size(83, 42);
            this.lblCurrentGraph.TabIndex = 2;
            this.lblCurrentGraph.Text = "N/A";
            // 
            // showGridCheckbox
            // 
            this.showGridCheckbox.AutoSize = true;
            this.showGridCheckbox.Enabled = false;
            this.showGridCheckbox.Location = new System.Drawing.Point(14, 64);
            this.showGridCheckbox.Name = "showGridCheckbox";
            this.showGridCheckbox.Size = new System.Drawing.Size(75, 17);
            this.showGridCheckbox.TabIndex = 3;
            this.showGridCheckbox.Text = "Show Grid";
            this.showGridCheckbox.UseVisualStyleBackColor = true;
            this.showGridCheckbox.CheckedChanged += new System.EventHandler(this.showGridCheckbox_CheckedChanged);
            // 
            // frmComparingSearchAlgorithms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 677);
            this.Controls.Add(this.btnCalculateAverage);
            this.Controls.Add(this.showGridCheckbox);
            this.Controls.Add(this.lblCurrentGraph);
            this.Controls.Add(this.txtDisplaying);
            this.Controls.Add(this.chGridofCities);
            this.Controls.Add(this.grpbxGenerateNetwork);
            this.Controls.Add(this.grpbxSearchForOptimalRoute);
            this.Controls.Add(this.grpbxPerformanceDisplay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmComparingSearchAlgorithms";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Performance Analysis of Optimisation Algorithms";
            this.Load += new System.EventHandler(this.frmComparingSearchAlgorithms_Load);
            this.grpbxGenerateNetwork.ResumeLayout(false);
            this.grpbxGenerateNetwork.PerformLayout();
            this.grpbxSearchForOptimalRoute.ResumeLayout(false);
            this.grpbxSearchForOptimalRoute.PerformLayout();
            this.grpbxPerformanceDisplay.ResumeLayout(false);
            this.grpbxPerformanceDisplay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chGridofCities)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart chGridofCities;
        private System.Windows.Forms.GroupBox grpbxGenerateNetwork;
        private System.Windows.Forms.GroupBox grpbxSearchForOptimalRoute;
        private System.Windows.Forms.GroupBox grpbxPerformanceDisplay;
        private System.Windows.Forms.Button btnBeginSearchAlgorithm;
        private System.Windows.Forms.Label lblSelectHeuristic;
        private System.Windows.Forms.ComboBox cbxSearchingAlgorithms;
        private System.Windows.Forms.TextBox txtNoOfNodes;
        private System.Windows.Forms.Label lblNumberOfNodes;
        private System.Windows.Forms.Button btnGenerate;
		private System.Windows.Forms.TextBox txtInitalDistance;
		private System.Windows.Forms.TextBox txtHillClimbing;
		private System.Windows.Forms.Label lblOptimiseTitle;
		private System.Windows.Forms.Label lblInitialDistance;
        private System.Windows.Forms.TextBox txtSimulatedAnnealing;
        private System.Windows.Forms.Label lblSimulatedAnnealing;
        private System.Windows.Forms.Label lblHillClimbing;
        private System.Windows.Forms.Button btnEditSearchOption;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblSelectGraphToView;
        private System.Windows.Forms.ComboBox cbxGraphToView;
        private System.Windows.Forms.Label lblStateOfSearch;
        private System.Windows.Forms.Label txtDisplaying;
        private System.Windows.Forms.Label lblCurrentGraph;
        private System.Windows.Forms.CheckBox showGridCheckbox;
        private System.Windows.Forms.Button btnCalculateAverage;
    }
}

