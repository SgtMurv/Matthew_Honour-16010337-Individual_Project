using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;  // so that we can add lines to the graph!

namespace Comparing_Search_Algorithms
{
	public partial class frmComparingSearchAlgorithms : Form
	{
        //Constants of the application:
        //graph generation:
        const int POINT_SERIES_NUMBER = 0;
        const int INVALID_NODE = -1;
        const int MAX_X_AXIS = 100;
		const int MIN_X_AXIS = 0;
		const int MAX_Y_AXIS = 100;
		const int MIN_Y_AXIS = 0;
        const int MAX_NUMBER_OF_NODES = 100;
        const int MIN_NUMBER_OF_NODES = 7;
        const int UPPER_BOUND_ARE_CONNECTED = 2;    //this needs to be 2 not 1 as the upper bound of the rand.Next() is exclusive (i.e. is never generated !)
        const int NOT_CONNECTED = 0;
        const int ARE_CONNECTED = 1;
        const int START_OF_LINES_INDEX = 1;         //setting this to be a constant as the lines in the series do not start until index 1 as index 0 is reserved for the nodes in the network.
        const int FIRST_ITEM_IN_COMBO_BOX = 0;
        const int UNSELECTED = -1;

        //search algorithms:
        const int NUMBER_OF_ITERATIONS_FOR_ALGORITHM_TO_RUN = 50;
        const int INFINITY = 2147483647;    //set to the largest number an integer can be.
        const int NUMBER_OF_NEIGHBOURS = 10;
        const int HILL_CLIMBING_INDEX = 0;
        const int SIMULATED_ANNEALING_INDEX = 1;
        const int ALL_ALGORITHMS_INDEX = 2;
        const String HILL_CLIMBING_STRING = "Hill Climbing";
        const String SIMULATED_ANNEALING_STRING = "Simulated Annealing";
        const String RANDOM_PATH_STRING = "Random Path";

        //Data Structures of the application:
        List <Node> nodeTable_RandomPath = new List<Node>();                         //node table for the random path so we can revert back to the random path when the user wants to switch search algorithms.   
        List<int> availableNodes = new List<int>();                                 //keeps track of all the nodes that the current node
        List<State> neighbourhoodOfStates = new List<State>();                      //List of States to store all of the neighbouring paths with their distances and their node Table.
        List<NodeCombination> nodeCombinations = new List<NodeCombination>();       //List of int arrays with two elements, each element represents a successful node swap when creating a new neighbour.

        //Class Variables:
        int numberOfPoints = 0; //int to store the number of points to be generated.
        int[] randomPath;       //This will store the randomly generated path
        State hillClimbingOptimisedState;       //stores the optimised state found by hill climbing
        State simulatedAnnealingOptimisedState; //stores the optimised state found by simulated annealing

        public frmComparingSearchAlgorithms()
		{
			InitializeComponent();
		}

        private void btnBeginSearchAlgorithm_Click(object sender, EventArgs e)
        {
            int selectedAlgorithm = cbxSearchingAlgorithms.SelectedIndex;
            switch (selectedAlgorithm)
            {

                case (HILL_CLIMBING_INDEX):
                    //update the label to show the user that the Graph is being optimised
                    lblStateOfSearch.Text = "Optimising path ...";                    

                    //disable the group box so the user knows that the heuristic is running.
                    grpbxSearchForOptimalRoute.Enabled = false;

                    //run the hill climbing search method.
                    runOptimisationAlgorithm(HILL_CLIMBING_STRING, this.hillClimbing);

                    //add the random path to the combo box
                    cbxGraphToView.Items.Add(RANDOM_PATH_STRING);

                    //enable the results groupbox once the search is complete
                    grpbxPerformanceDisplay.Enabled = true;

                    //update the label to show the user that the algorithms are done
                    lblStateOfSearch.Text = "Done!";

                    //set the selected item of the combobox to be the first item so that the graph gets updated.
                    cbxGraphToView.SelectedIndex = FIRST_ITEM_IN_COMBO_BOX;

                    break;
                case (SIMULATED_ANNEALING_INDEX):
                    //update the label to show the user that the Graph is being optimised
                    lblStateOfSearch.Text = "Optimising path ...";

                    //disable the group box so the user knows that the heuristic is running.
                    grpbxSearchForOptimalRoute.Enabled = false;

                    //run the simulated annealing search method.
                    runOptimisationAlgorithm(SIMULATED_ANNEALING_STRING, this.simulatedAnnealing);

                    //add the random path to the combo box
                    cbxGraphToView.Items.Add(RANDOM_PATH_STRING);

                    //enable the results groupbox once the search is complete
                    grpbxPerformanceDisplay.Enabled = true;

                    //update the label to show the user that the algorithms are done
                    lblStateOfSearch.Text = "Done!";

                    //set the selected item of the combobox to be the first item so that the graph gets updated.
                    cbxGraphToView.SelectedIndex = FIRST_ITEM_IN_COMBO_BOX;

                    break;
                case (ALL_ALGORITHMS_INDEX):
                    //update the label to show the user that the Graph is being optimised
                    lblStateOfSearch.Text = "Optimising path ...";

                    //disable the group box so the user knows that the heuristic is running.
                    grpbxSearchForOptimalRoute.Enabled = false;

                    //call the method that takes care of the logic encorportated with running all of the methods.
                    this.runAllAlgorithms();

                    //add the random path to the combo box
                    cbxGraphToView.Items.Add(RANDOM_PATH_STRING);

                    //enable the results groupbox once the search is complete
                    grpbxPerformanceDisplay.Enabled = true;

                    //update the label to show the user that the algorithms are done
                    lblStateOfSearch.Text = "Done!";

                    //set the selected item of the combobox to be the first item so that the graph gets updated.
                    cbxGraphToView.SelectedIndex = FIRST_ITEM_IN_COMBO_BOX;

                    break;
                default:
                    MessageBox.Show("You must select an option from the combo box above!", "Error when optimising graph!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void cbxGraphToView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //clear the series in the current graph view
            chGridofCities.Series.Clear();

            //need to add a new series to the chart for the points
            addPointSeriesToChart();

            //plot the nodes back on the graph
            for (int i = 0; i < this.nodeTable_RandomPath.Count(); i++)
            {
                //add the node from the node table back onto the graph
                chGridofCities.Series[0].Points.AddXY(this.nodeTable_RandomPath[i].getXValue(), this.nodeTable_RandomPath[i].getYValue());
            }

            //determine what optimised graph the user wants to view
            String selectedGraph = cbxGraphToView.Text;
            switch (selectedGraph)
            {
                case (HILL_CLIMBING_STRING):
                    //we need to use the node table of the optimal state that hill climbing discovered to re-connect all of the nodes
                    connectAllNodes(this.hillClimbingOptimisedState.getNodeTable());
                    //update lblCurrentGraph
                    lblCurrentGraph.Text = HILL_CLIMBING_STRING;
                    break;
                case (SIMULATED_ANNEALING_STRING):
                    //we need to use the node table of the optimal state that simulated annealing discovered to re-connect all of the nodes
                    connectAllNodes(this.simulatedAnnealingOptimisedState.getNodeTable());
                    //update lblCurrentGraph
                    lblCurrentGraph.Text = SIMULATED_ANNEALING_STRING;
                    break;
                case (RANDOM_PATH_STRING):
                    //use the node table of the random path to repopulate the graph
                    connectAllNodes(this.nodeTable_RandomPath);
                    //update lblCurrentGraph
                    lblCurrentGraph.Text = RANDOM_PATH_STRING;
                    break;
            }
        }

        private void btnEditSearchOption_Click(object sender, EventArgs e)
        {
            //disable the results groupbox
            grpbxPerformanceDisplay.Enabled = false;
            //enable the search groupbox
            grpbxSearchForOptimalRoute.Enabled = true;

            //change the selected item of cbxGraphToView to random path so that the random path is shown again when the search is reset
            cbxGraphToView.SelectedIndex = cbxGraphToView.FindStringExact(RANDOM_PATH_STRING);

            clearTextOnControlsForNewSearch();

            //need to reset the data structures so that we can run the search on the same random path again.
            resetEnvironmentForNextSearch();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            resetApplication();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
		{
            //Check if txtNumberOfNodes is empty
            if(txtNoOfNodes.Text == "")
            {
                MessageBox.Show("You must enter the number of nodes to be generated onto the graph.", "Error when generating graph!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //Trying to parse the string from txtNoOfNodes to an int.
                if (int.TryParse(txtNoOfNodes.Text, out this.numberOfPoints))
                {
                    //ensures the user doesnt enter a very large number and slows down the system by doing so.
                    //There needs to be at least 7 nodes as any less then there would not be enough unique combinations of node swaps to generate the neighbourhood of states.
                    if (numberOfPoints > MAX_NUMBER_OF_NODES + 1 || numberOfPoints < MIN_NUMBER_OF_NODES)
                    {
                        MessageBox.Show("Error you must enter a number between 7 and 100! Please try again.", "Error when generating graph!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        txtNoOfNodes.Text = "";
                    }
                    else
                    {
                        //need to add a new series to the chart
                        addPointSeriesToChart();
                        //generate random nodes onto chGridofCities.
                        this.generateNodes();
                        //Call generateRandomPath here so the nodes get connected once they are generated.
                        this.generateRandomPath();

                        lblCurrentGraph.Text = RANDOM_PATH_STRING;

                        btnCalculateAverage.Enabled = true;
                        showGridCheckbox.Enabled = true;
                        //Disable grpbxGenerateNetwork so that the user cannot generate two graphs on the same chart object.
                        grpbxGenerateNetwork.Enabled = false;
                        //Enable grpbxSearchForOptimalRoute
                        grpbxSearchForOptimalRoute.Enabled = true;
                    }
                }
                //if the parse was unsuccessful then an error is thrown.
                else
                {
                    MessageBox.Show("Error input was not an Integer!", "Error when generating graph!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    txtNoOfNodes.Text = "";
                }
            }
        }

        //Methods of the class:

        void resetApplication()
        {
            //need to reset all of the data structures in the application.
            this.nodeTable_RandomPath.Clear();
            this.neighbourhoodOfStates.Clear();
            this.nodeCombinations.Clear();
            this.hillClimbingOptimisedState = null;
            this.simulatedAnnealingOptimisedState = null;
            Array.Clear(this.randomPath, 0, this.randomPath.Count());

            //need to remove all of the edges and nodes from chGridofCities.
            chGridofCities.Series.Clear();

            //clear text in controls for the new graph to be generated
            txtInitalDistance.Text = "";
            txtNoOfNodes.Text = "";

            clearTextOnControlsForNewSearch();

            btnCalculateAverage.Enabled = false;
            showGridCheckbox.Enabled = false;
            showGridCheckbox.Checked = false;

            //disable the latter 2 group boxes and enable the generate graph groupbox.
            grpbxGenerateNetwork.Enabled = true;
            grpbxSearchForOptimalRoute.Enabled = false;
            grpbxPerformanceDisplay.Enabled = false;
        }

        void runOptimisationAlgorithm(String algorithmName, Action optimisationAlgorithm)
        {
            //run the algorithm
            optimisationAlgorithm();
            //add the option to cbxGraphToView
            cbxGraphToView.Items.Add(algorithmName);
        }

        //clear the common controls that need to be cleared when resetting and editing the search method
        void clearTextOnControlsForNewSearch()
        {
            //need to clear the populated controls ready for the next search
            txtHillClimbing.Text = "";
            txtSimulatedAnnealing.Text = "";
            lblStateOfSearch.Text = "";

            //need to remove the selection from cbxSearchingAlgorithms
            cbxSearchingAlgorithms.SelectedIndex = UNSELECTED;

            //need to remove all of the items from cbxGraphToView
            cbxGraphToView.Items.Clear();
        }

        //add the point chart series onto the chart view
        void addPointSeriesToChart()
        {
            //need to add a new point series to the chart
            Series pointChart = new Series();
            pointChart.ChartType = SeriesChartType.Point;
            chGridofCities.Series.Add(pointChart);
            chGridofCities.Series[POINT_SERIES_NUMBER].IsVisibleInLegend = false;
            chGridofCities.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chGridofCities.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            chGridofCities.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chGridofCities.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
        }

        //method to connect all of the nodes on the graph using a node table from the parameter list.
        void connectAllNodes(List<Node> nodeTable)
        {
            for(int i = 0; i < nodeTable.Count(); i++)
            {
                connectTwoNodes(i, nodeTable, i, nodeTable[i].getNext());
            }
        }

        //method to run all of the algorithms on the randomly generated graph
        private void runAllAlgorithms()
        {
            //run the hill-climbing search method.
            runOptimisationAlgorithm(HILL_CLIMBING_STRING, this.hillClimbing);
            //need to reset the data structures so that we can run the search on the same random path again.
            resetEnvironmentForNextSearch();
            //run the simulated annealing search method.
            runOptimisationAlgorithm(SIMULATED_ANNEALING_STRING, this.simulatedAnnealing);
        }

        //run when we want to run another algorithm after already running an algorithm
        private void resetEnvironmentForNextSearch()
        {
            //need to reset the data structures so that we can run the search on the same random path again.
            this.neighbourhoodOfStates.Clear();
            this.nodeCombinations.Clear();
        }

        private void copyNodeTable(List<Node> sourceNodeTable, List<Node> destinationNodeTable)
        {
            foreach (Node node in sourceNodeTable)
            {
                Node copyOfNode = node.copyNode();
                destinationNodeTable.Add(copyOfNode);
            }
        }

        //run to check if the node being generated is not already on the graph.
        private bool doesNodeExists(int xValue, int yValue)
        {
            //default set to false(is a unique value) so will only change to true if the point does exist on the graph already, therfore no need for else statement to set to false.
            bool resultOfCheck = false;
            //iterates through the node_table
            for (int i = 0; i < this.nodeTable_RandomPath.Count; i++)  
            {
                if (this.nodeTable_RandomPath[i].getXValue() == xValue || this.nodeTable_RandomPath[i].getYValue() == yValue)
                {
                    resultOfCheck = true;
                }
            }
            return resultOfCheck;
        }

        //purpose of this method is to add all the nodes in node table that have not yet been visited to a list of availableNodes.
        public bool determineAvailableNodes()
        {
            this.availableNodes.Clear();
            bool allNodesHaveBeenVisited = true;
            for (int i = 0; i< this.nodeTable_RandomPath.Count();i++)
            {
                if(!this.nodeTable_RandomPath[i].getVisited())
                {
                    this.availableNodes.Add(i);
                    if(allNodesHaveBeenVisited)            //The boolean is only changed to false if a node is added to the list.
                    {
                        allNodesHaveBeenVisited = false;
                    }
                }
            }
            return allNodesHaveBeenVisited;
        }

        private void generateNodes()
        {
            //random object used to randomly generate values for x and y for each node.
            Random rand = new Random();

            //loops as many times as there are points entered by the user
            for (int i = 0; i < numberOfPoints; i++)
            {
                int xValue = rand.Next(MIN_X_AXIS, MAX_X_AXIS);
                int yValue = rand.Next(MIN_Y_AXIS, MAX_Y_AXIS);

                //if the node table is not empty
                if (this.nodeTable_RandomPath.Count != 0)
                {
                    //if the node does exist on the graph already
                    if (doesNodeExists(xValue, yValue))
                    {
                        //decrement the i value so that the loop will go on for one extra iteration, this means it will continue to iterate and try to add unique nodes until they are unique.
                        i--;
                    }
                    //if the node doesn't exist on the graph already
                    else
                    {
                        //Adds a random point to the graph.
                        chGridofCities.Series[POINT_SERIES_NUMBER].Points.AddXY(xValue, yValue);      
                        //creating a new node for that random node in the graph and then adding it to the nodeTable_RandomPath.
                        Node newNode = new Node(xValue, yValue);
                        this.nodeTable_RandomPath.Add(newNode);
                    }
                }
                //if the nodeTable_RandomPath is empty then any value added will not already be present on the graph so the check is not necessary.
                else
                {
                    //Adds a random point to the graph.
                    chGridofCities.Series[POINT_SERIES_NUMBER].Points.AddXY(xValue, yValue);
                    //creating a new node for that random node in the graph and then adding it to the nodeTable_RandomPath.
                    Node newNode = new Node(xValue, yValue);
                    this.nodeTable_RandomPath.Add(newNode);
                }
            }
        }

        public void generateRandomPath()
        {
            const bool VISITED = true;
            int startNode = 0;                                          //setting the start node to always be the first in the node table, Maybe let the user specify this at a later date.
            int currentNode = startNode;
            int nextNode = INVALID_NODE;
            this.randomPath = new int[this.numberOfPoints];

            this.nodeTable_RandomPath[startNode].setVisited(true);     //so that we do not connect the nodes to the start node again.
            
            Random rand = new Random();
            int series_counter = 0;                                     //keeps track of the number of series of lines on the graph

            int nodeCounter = 0;

            while (!this.determineAvailableNodes() || currentNode != startNode)
            {
                //randomly pick the next node out of the list of unvisited nodes
                if (this.availableNodes.Count() > 0)
                {
                    int nextNodeIndex = rand.Next(0, this.availableNodes.Count() - 1);
                    nextNode = this.availableNodes[nextNodeIndex];
                    this.nodeTable_RandomPath[nextNode].setVisited(VISITED); //not in both cases as the start node has already been visited
                }
                else    //when there are no more unvisited nodes
                {
                    nextNode = startNode;  //we need to connect up the start node to this current node (i.e. the last node)
                }
                //set the previous to the current node.
                this.nodeTable_RandomPath[nextNode].setPrevious(currentNode);  //sets the previous node of the next node to be the current node.
                this.nodeTable_RandomPath[currentNode].setNext(nextNode);      //sets the next node of the current node to be the next node.
                this.randomPath[nodeCounter] = currentNode;                     //adds the current node to the randomly generated path array.
                nodeCounter++;

                //connect the two points on the graph:
                connectTwoNodes(series_counter, this.nodeTable_RandomPath, currentNode, nextNode);

                series_counter++;          //new series added to counter is incremented

                currentNode = nextNode;    //current node equals next node so that the next time the loop runs we are trying to connect the next node up to an unvisited node.
            }
            //display the distance of the random path in txtInitialDistance.
            txtInitalDistance.Text = Convert.ToString(this.getTotalDistanceOfPath(this.randomPath, this.nodeTable_RandomPath));
        }

        //method to connect two nodes on a graph
        void connectTwoNodes(int seriesCounter, List<Node> nodeTable, int currentNode, int nextNode)
        {
            //connect the two points on the graph:
            chGridofCities.Series.Add("" + seriesCounter);
            chGridofCities.Series["" + seriesCounter].Points.Add(new DataPoint(nodeTable[currentNode].getXValue(), nodeTable[currentNode].getYValue()));
            chGridofCities.Series["" + seriesCounter].Points.Add(new DataPoint(nodeTable[nextNode].getXValue(), nodeTable[nextNode].getYValue()));
            chGridofCities.Series["" + seriesCounter].ChartType = SeriesChartType.Line;
            chGridofCities.Series["" + seriesCounter].IsVisibleInLegend = false;   //make it invisible on the actual graph.
        }

        double getTotalDistanceOfPath(int[] path, List<Node> node_table)
        {
            double totalDistance = 0.0;

            for(int i =0; i < path.Count()-1; i ++) //Count()-1 as it is an array (so starts at 0) and we want to loop as many times as there are items in the array
            {
                int currentNode = path[i];  //the first entry in the path is the starting node.
                int nextNode = node_table[currentNode].getNext();
                //use pythag to calculate the distance between the two nodes:
                double baseOfTriangle = node_table[currentNode].getXValue() - node_table[nextNode].getXValue();
                double heightOfTriangle = node_table[currentNode].getYValue() - node_table[nextNode].getYValue();
                double distanceBetweenNodes = Math.Sqrt(Math.Pow(baseOfTriangle, 2) + Math.Pow(heightOfTriangle, 2));       //calculates the distance of the edge between the two nodes using pythagorus's theorum.

                //adds the distance between the two nodes to the totalDistance.
                totalDistance += distanceBetweenNodes;
            }
            return totalDistance;
        }

        void generateNeighbour(State currentState)
        {
            //create a local copy of the current path to work from so that we do not directly edit the current path on the graph.
            int[] currentPathCopy = new int[currentState.getPathObject().getPath().Count()];
            Array.Copy(currentState.getPathObject().getPath(), currentPathCopy, currentState.getPathObject().getPath().Count());
            //nodeTable_NewNeighbour will store the node values of every node in the current new neighbour.
            List<Node> nodeTable_NewNeighbour = new List<Node>();

            //set the variables for use within the do, while loop
            int node1;
            int node1IndexInCurrentPathCopy;
            int node2;
            int node2IndexInRemainingNodes;

            //copying the nodes that node2 can be into another array, so that the rand.Next method can be used to randomly select node2.
            //-3 as node2 cannot be related to node1 in any way (i.e. node1 != (node1.getPrev || node1.getNext))
            int[] remainingNodes = new int[currentPathCopy.Count() - 3];
            int remainingNodeIndex;

            //make a copy of the node table of the current state so that nodeTable_NewNeighbour is ready to be edited.
            this.copyNodeTable(currentState.getNodeTable(), nodeTable_NewNeighbour);

            do
            {
                //randomly choose one node from node table and store it in a variable called node1.
                Random rand = new Random();
                node1IndexInCurrentPathCopy = rand.Next(0, currentPathCopy.Count() - 1);
                node1 = currentState.getPathObject().getPath()[node1IndexInCurrentPathCopy];

                //clear the array just in case this has to be run again due to a repeat combination being generated.
                Array.Clear(remainingNodes, 0, remainingNodes.Count());
                remainingNodeIndex = 0;
                for (int i = 0; i < currentPathCopy.Count(); i++)
                {
                    if (currentPathCopy[i] != node1 &&
                        currentPathCopy[i] != currentState.getNodeTable()[node1].getPrevious() &&
                        currentPathCopy[i] != currentState.getNodeTable()[node1].getNext())
                    {
                        remainingNodes[remainingNodeIndex] = currentPathCopy[i];
                        remainingNodeIndex++;
                    }
                }
                //randomly choose another node from node table and store it in node2.
                node2IndexInRemainingNodes = rand.Next(0, remainingNodes.Count() - 1);
                node2 = remainingNodes[node2IndexInRemainingNodes];
            }
            //adding the do while to run the check method after the nodes have been generated at least once.
            while (!this.checkIfNodeCombinationIsUnique(node1,node2));

            //add the new unique combination to the combinations data structure.
            this.nodeCombinations.Add(new NodeCombination(node1,node2));

            //store node1.next and node1.prev in variables.
            int originalNode1_Next = currentState.getNodeTable()[node1].getNext();
            int originalNode1_Previous = currentState.getNodeTable()[node1].getPrevious();

            //need to also set the attributes of the nodes surrounding node 1 and node 2:

            //set the next attribute of the previous node to node 1 to be equal to node 2
            nodeTable_NewNeighbour[currentState.getNodeTable()[node1].getPrevious()].setNext(node2);
            //set the prev attribute of the next node to node 1 to be equal to node 2
            nodeTable_NewNeighbour[currentState.getNodeTable()[node1].getNext()].setPrevious(node2);
            //set the next attribute of the previous node to node 2 to be equal to node 1
            nodeTable_NewNeighbour[currentState.getNodeTable()[node2].getPrevious()].setNext(node1);
            //set the prev attribute of the next node to node 2 to be equal to node 1
            nodeTable_NewNeighbour[currentState.getNodeTable()[node2].getNext()].setPrevious(node1);

            //set node1.next = node2.next
            nodeTable_NewNeighbour[node1].setNext(currentState.getNodeTable()[node2].getNext());
            //set node1.prev = node2.prev
            nodeTable_NewNeighbour[node1].setPrevious(currentState.getNodeTable()[node2].getPrevious());
            
            //set node2.next = originalNode1_Next
            nodeTable_NewNeighbour[node2].setNext(originalNode1_Next);
            //set node2.prev = originalNode1_Previous
            nodeTable_NewNeighbour[node2].setPrevious(originalNode1_Previous);

            //get the index of node 2 in the currentPathCopy rather than the index in remaining nodes
            int node2IndexInCurrentPathCopy = 0;
            for(int i =0; i < currentPathCopy.Count()-1;i++)
            {
                if(currentPathCopy[i] == node2)
                {
                    node2IndexInCurrentPathCopy = i;
                    break;
                }
            }

            //swap node1 and node2 in the path.
            currentPathCopy[node1IndexInCurrentPathCopy] = node2;
            currentPathCopy[node2IndexInCurrentPathCopy] = node1;

            //initialise a State object with the current path and the total distance of that path (by calling getTotalDistanceOfPath) and the node table new neighbour object
            //This State object is then added to the neighbourhoodOfStates data structure.
            this.neighbourhoodOfStates.Add(new State(new Path(currentPathCopy, this.getTotalDistanceOfPath(currentPathCopy, nodeTable_NewNeighbour), this.numberOfPoints),nodeTable_NewNeighbour));

            //reset the node_table so the next and prev values of each node match that of the random path.
            nodeTable_NewNeighbour.Clear();
        }
        bool checkIfNodeCombinationIsUnique(int node1, int node2)
        {
            //set a variable that keeps track of whether the generated node combination is unique.
            bool resultOfCheck = true;

            //loop through the first values in each element in the nodeCombinations data structure and check they are unique
            for(int i =0; i< this.nodeCombinations.Count(); i++)
            {
                //if there is a node in nodeCombinations that has the same node1 and node2 then set the bool variable to false.
                if (node1 == this.nodeCombinations[i].getNode1() && node2 == this.nodeCombinations[i].getNode2() || node1 == this.nodeCombinations[i].getNode2() && node2 == this.nodeCombinations[i].getNode1())
                {
                    resultOfCheck = false;
                }
            }
            //return the boolean value
            return resultOfCheck;
        }

        //we need to be able to generate a neighbourhood of States for our heuristics to search through.
        void generateNeighbourhoodOfStates(State currentState)    
        {
            //Loop ten times as we will have ten neighbours
            for (int i =0; i < NUMBER_OF_NEIGHBOURS; i++)
            {
                //call the generate neighbour method, passing in the current Path.
                this.generateNeighbour(currentState);
            }
        }

        //method to transistion from the current state to the most improving state in the neighbourhood.
        void moveStates(State currentState, State neighbour)
        {
            //this neighbour has a more optimal route than the current shortest path, so we set the path and distance of the path of the shortest path to be the values of the neighbour:
            currentState.getPathObject().setPath(neighbour.getPathObject().getPath());
            currentState.getPathObject().setDistance(neighbour.getPathObject().getDistanceOfPath());
            //we then have to set the value of the node table of the current state to be the node table of that current neighbour
            currentState.getNodeTable().Clear();
            currentState.setNodeTable(neighbour.getNodeTable());
        }

        //method containing a prototype for the hill climbing algorithm.
        void hillClimbing()
        {
            //create a State object variable to store the distance of the shortest path and the path (at the start this will be the random path) and the node table of that path.
            State currentState = new State(new Path(this.randomPath, this.getTotalDistanceOfPath(this.randomPath, this.nodeTable_RandomPath), this.numberOfPoints), this.nodeTable_RandomPath);

            bool theAlgorithmIsStuckInALocalOptima;

            //loop through the algorithm a set number of times.
            for (int i = 0; i < NUMBER_OF_ITERATIONS_FOR_ALGORITHM_TO_RUN; i++)
            {
                //set the flag  of theAlgorithmIsStuckInALocalOptima to true for the first running of the algorithm for each iteration
                theAlgorithmIsStuckInALocalOptima = true;

                //generate a neighbourhood of states for the current state that the path is in, passing in the current state
                this.generateNeighbourhoodOfStates(currentState);

                //loop through the neighbourhood of states and select a new current state if the criteria are met
                foreach (State neighbour in this.neighbourhoodOfStates)
                {
                    //if the distance of the neighbouring path is less than the distance of the current shorest path, then:
                    if (neighbour.getPathObject().getDistanceOfPath() < currentState.getPathObject().getDistanceOfPath())
                    {
                        //move from the current state to the nieghbouring state
                        moveStates(currentState,neighbour);

                        //set the theAlgorithmIsStuckInALocalOptima flag to false as if this code is run then a neighbour exists that has a total distance less than that of the current state.
                        theAlgorithmIsStuckInALocalOptima = false;
                    }
                }

                //if no change occured when looking through the neighbours of the state then we terminate the search (i.e. break out of the for loop)
                if (theAlgorithmIsStuckInALocalOptima == true)
                {
                    break;
                }

                //empty the neighbourhood of states and nodeComcinations, ready for population again.
                this.neighbourhoodOfStates.Clear();
                this.nodeCombinations.Clear();
            }
            //update the textbox to the user showing them the distance of the path that hill-climbing found
            txtHillClimbing.Text = (currentState.getPathObject().getDistanceOfPath()).ToString();

            //set the hillClimbingOptimisedState to the current state.
            this.hillClimbingOptimisedState = new State(currentState.getPathObject(), currentState.getNodeTable());
        }

        //method containing a prototype for the simulated annealing algorithm.
        void simulatedAnnealing()
        {
            //create a State object variable to store the distance of the shortest path and the path (at the start this will be the random path) and the node table of that path.
            State currentState = new State(new Path(this.randomPath, this.getTotalDistanceOfPath(this.randomPath, this.nodeTable_RandomPath), this.numberOfPoints), this.nodeTable_RandomPath);

            double PImprovingState = 60;
            double Temperature = 40;

            //need to pre define the amount that the temperature will decrease by at each iteration.
            double amountToDecreaseTemperature = Temperature / (NUMBER_OF_ITERATIONS_FOR_ALGORITHM_TO_RUN);

            //loop through the algorithm a set number of times.
            for (int i = 0; i < NUMBER_OF_ITERATIONS_FOR_ALGORITHM_TO_RUN; i++)
            {
                //Randomly select a number between 0 and 100.
                Random rand = new Random();
                int randomValue = rand.Next(0, 100);

                //we need to generate a neighbourhood of states based off the current state.
                this.generateNeighbourhoodOfStates(currentState);

                //determine whether we are selecting an improving state or a non-improving state
                if (randomValue <= PImprovingState)
                { 
                    foreach (State neighbour in this.neighbourhoodOfStates)
                    {
                        //if the distance of the neighbouring path is less than the distance of the current shorest path, then:
                        if (neighbour.getPathObject().getDistanceOfPath() < currentState.getPathObject().getDistanceOfPath())
                        {
                            //we loop this method as we are finding the most optimal neighbour
                            //move from the current state to the nieghbouring state
                            moveStates(currentState, neighbour);
                        }
                    }
                }
                else
                {
                    foreach (State neighbour in this.neighbourhoodOfStates)
                    {
                        //if the distance of the neighbouring path is less than the distance of the current shorest path, then:
                        if (neighbour.getPathObject().getDistanceOfPath() > currentState.getPathObject().getDistanceOfPath())
                        {
                            //move from the current state to the nieghbouring state
                            moveStates(currentState, neighbour);

                            //exit the foreach loop here as we are moving to the first neighbouring state that satisfies these conditions.
                            break;
                        }
                    }
                }

                //need to decrease the temperature by a certain amount based on the value of the NUMBER_OF_ITERATIONS_FOR_ALGORITHM_TO_RUN constant only if Temperature is not already 0
                if(Temperature != 0)
                {
                    Temperature -= amountToDecreaseTemperature;
                    PImprovingState += amountToDecreaseTemperature;
                }
                //empty the neighbourhood of states and nodeCombinations, ready for population again.
                this.neighbourhoodOfStates.Clear();
                this.nodeCombinations.Clear();
            }
            //update the textbox to the user showing them the distance of the path that simulated-annealing found
            txtSimulatedAnnealing.Text = (currentState.getPathObject().getDistanceOfPath()).ToString();

            //set the simulatedAnnealingOptimisedState to the current state.
            this.simulatedAnnealingOptimisedState = new State(currentState.getPathObject(), currentState.getNodeTable());
        }

        private void frmComparingSearchAlgorithms_Load(object sender, EventArgs e)
        {
            //setting certain textboxes and comboboxes to read only
            txtHillClimbing.ReadOnly = true;
            txtInitalDistance.ReadOnly = true;
            txtSimulatedAnnealing.ReadOnly = true;
        }

        private void showGridCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (showGridCheckbox.Checked == true)
            {
                chGridofCities.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
                chGridofCities.ChartAreas[0].AxisX.MinorGrid.Enabled = true;
                chGridofCities.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
                chGridofCities.ChartAreas[0].AxisY.MinorGrid.Enabled = true;
            }
            else
            {
                chGridofCities.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chGridofCities.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
                chGridofCities.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                chGridofCities.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
            }
        }

        //tests the performance of HC and SA.
        private void btnCalculateAverage_Click(object sender, EventArgs e)
        {
            this.resetEnvironmentForNextSearch();

            double[] averageHillClimbingDistanceArray = calculateAverageDistance(HILL_CLIMBING_STRING, this.hillClimbing, txtHillClimbing);
            double[] averageSimulatedAnnealingDistanceArray = calculateAverageDistance(SIMULATED_ANNEALING_STRING, this.simulatedAnnealing, txtSimulatedAnnealing);
            String HCoutputMessage = "";
            String SAoutputMessage = "";
            double totalHC = 0.0;
            double totalSA = 0.0;

            for (int i = 0; i < averageHillClimbingDistanceArray.Count(); i++)
            {
                HCoutputMessage += "[" + i + 1 + "] -> " + averageHillClimbingDistanceArray[i] + Environment.NewLine;
                totalHC += averageHillClimbingDistanceArray[i];
                SAoutputMessage += "[" + i + 1 + "] -> " + averageSimulatedAnnealingDistanceArray[i] + Environment.NewLine;
                totalSA += averageSimulatedAnnealingDistanceArray[i];
            }

            double HCaveragePercentageDecrease = totalHC / 20;
            double SAaveragePercentageDecrease = totalSA / 20;

            HCoutputMessage += Environment.NewLine + "Average Percentage Decrease = " + HCaveragePercentageDecrease;
            SAoutputMessage += Environment.NewLine + "Average Percentage Decrease = " + SAaveragePercentageDecrease;

            MessageBox.Show(HCoutputMessage);
            MessageBox.Show(SAoutputMessage);

            resetApplication();
        }

        double[] calculateAverageDistance(String algorithmName, Action optimisationAlgorithm, TextBox distanceOfOptimsedPathTextBox)
        {
            //run each algorithm 20 times on the same graph to calculate the average
            int numberOfRuns = 20;
            double[] decreaseInDistances = new double[numberOfRuns];

            for (int i = 0;i< numberOfRuns; i++)
            {
                this.runOptimisationAlgorithm(algorithmName, optimisationAlgorithm);

                double distanceOfRandomPath = double.Parse(txtInitalDistance.Text);
                double distanceOfOptimisedPath = double.Parse(distanceOfOptimsedPathTextBox.Text);

                decreaseInDistances[i] = ((distanceOfRandomPath - distanceOfOptimisedPath) / distanceOfRandomPath) *100;
                this.resetEnvironmentForNextSearch();
            }

            return decreaseInDistances;
        }
    }
    public class Node
	{
        const int INVALID_NODE = -1;

		private int xValue = 0;
		private int yValue = 0;
        private bool visited = false;
        private int previous = INVALID_NODE;    //once we connect two nodes together we set the previous attribute of the next node to be the current node
        private int nextNode = INVALID_NODE;

		public Node(int xValue, int yValue) //sets the value of the node with the passed in arguments.
		{
			this.xValue = xValue;
			this.yValue = yValue;
		}
		public int getXValue()           //enables other methods to get the xValue of the current node in question.
		{
			return this.xValue;
		}
		public int getYValue()           //enables other methods to get the yValue of the current node in question.
		{
			return this.yValue;
		}
        public void setPrevious(int previous)
        {
            this.previous = previous;
        }
        public int getPrevious()
        {
            return this.previous;
        }
        public void setNext(int nextNode)
        {
            this.nextNode = nextNode;
        }
        public int getNext()
        {
            return this.nextNode;
        }
        public void setVisited(bool visited)
        {
            this.visited = visited;
        }
        public bool getVisited()
        {
            return this.visited;
        }
        public Node copyNode()
        {
            Node copyOfNode = new Node(this.getXValue(), this.getYValue());
            copyOfNode.setNext(this.getNext());
            copyOfNode.setPrevious(this.getPrevious());
            copyOfNode.setVisited(this.getVisited());
            return copyOfNode;
        }
	}

    public class Path
    {
        //class attributes:
        private int[] path;
        private double distanceOfPath = 0;

        public Path(int[] path, double distanceOfPath, int numberOfNodes)
        {
            //set the size of the path to be the value passed into the constructor.
            this.path = new int[numberOfNodes];

            //initialise attributes with parameters.
            Array.Copy(path, this.path, path.Count());
            this.distanceOfPath = distanceOfPath;
        }
        public double getDistanceOfPath()
        {
            return this.distanceOfPath;
        }
        public int[] getPath()
        {
            return this.path;
        }
        public void setDistance(double distanceOfPath)
        {
            this.distanceOfPath = distanceOfPath;
        }
        public void setPath(int[] path)
        {
            //clear the current value of the path
            Array.Clear(this.path, 0, this.path.Count());
            //copy the path passed as a parameter to the class attribute of the path
            Array.Copy(path, this.path, path.Count());
        }
    }
    public class NodeCombination
    {
        private int node1;
        private int node2;
        public NodeCombination(int node1, int node2)
        {
            this.node1 = node1;
            this.node2 = node2;
        }
        public int getNode1()
        {
            return this.node1;
        }
        public int getNode2()
        {
            return this.node2;
        }
    }

    public class State
    {
        private Path path;
        private List<Node> nodeTable = new List<Node>();
        public State(Path path, List<Node> nodeTable)
        {
            this.setPathObject(path);
            this.setNodeTable(nodeTable);
        }
        public void setNodeTable(List<Node> nodeTable)
        {
            foreach (Node node in nodeTable)
            {
                Node copyOfNode = node.copyNode();
                this.nodeTable.Add(copyOfNode);
            }
        }
        public void setPathObject(Path path)
        {
            this.path = path;
        }
        public List<Node> getNodeTable()
        {
            return this.nodeTable;
        }
        public Path getPathObject()
        {
            return this.path;
        }
    }
}
