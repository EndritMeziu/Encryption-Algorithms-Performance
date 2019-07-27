/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package encryptionalgorithmsperformance;

import java.awt.event.MouseEvent;
import java.net.URL;
import java.util.ResourceBundle;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.chart.PieChart;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.layout.Background;
import javafx.scene.layout.Pane;

/**
 *
 * @author USER
 */
public class FXMLDocumentController implements Initializable {
    
    @FXML
    private Label lblText;
    @FXML
    private Pane pnlHome,pnlOthers;
    @FXML
    private Button btnHome,btnOthers;
   
    @FXML
    private PieChart pieChart;
    
    
    @FXML
    private void handleButtonAction(ActionEvent event) {
        
    }
    
    @FXML
    private void HomeClick(ActionEvent event)
    {
        pnlHome.toFront(); 
        ObservableList<PieChart.Data> pieChartData = 
                FXCollections.observableArrayList(
                    new PieChart.Data("January", 100),
                    new PieChart.Data("February", 200),
                    new PieChart.Data("March", 50),
                    new PieChart.Data("April", 75),
                    new PieChart.Data("May", 110));
     
        pieChart.setData(pieChartData);
    }
    
    @FXML
    private void OtherClick(ActionEvent event)
    {
        pnlOthers.toFront();    
    }
    
    @Override
    public void initialize(URL url, ResourceBundle rb) {
        // TODO
        
    }    
    
}
