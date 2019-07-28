/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package encryptionalgorithmsperformance;

import com.sun.javafx.geom.Shape;
import java.io.File;
import java.io.IOException;
import java.io.PrintWriter;
import java.math.BigDecimal;
import java.net.URL;
import java.security.NoSuchAlgorithmException;
import java.security.SecureRandom;
import java.util.ArrayList;
import java.util.Random;
import java.util.ResourceBundle;
import javafx.beans.binding.Bindings;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.geometry.Side;
import javafx.scene.chart.BarChart;
import javafx.scene.chart.CategoryAxis;
import javafx.scene.chart.NumberAxis;
import javafx.scene.chart.PieChart;
import javafx.scene.chart.XYChart;
import javafx.scene.control.Button;
import javafx.scene.control.ComboBox;
import javafx.scene.control.Label;
import static javafx.scene.input.KeyCode.X;
import static javafx.scene.input.KeyCode.Y;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.Pane;
import javax.crypto.KeyGenerator;
import javax.crypto.SecretKey;
import static jdk.nashorn.internal.objects.NativeMath.round;

/**
 *
 * @author USER
 */
public class FXMLDocumentController implements Initializable {
    
    byte[] IV;
    SecretKey key;
    File encrypted;
    File decrypted;
    double encryptionSum;
    double decryptionSum;
    double encLoadSum;
    double decLoadSum;
    double keyGeneration;
    
    boolean once = true;
    
    ArrayList<String> encryptionTime = new ArrayList<String>(); 
    ArrayList<String> decryptionTime = new ArrayList<String>(); 
    ArrayList<String> encryptionLoad = new ArrayList<String>(); 
    ArrayList<String> decryptionLoad = new ArrayList<String>();
    XYChart.Series series = new XYChart.Series();
    CategoryAxis xAxis = new CategoryAxis();
    NumberAxis yAxis = new NumberAxis();
    
    float encTime;
    float decTime;
    float encLoad;
    float decLoad;
    String AlgorithmModel;
    @FXML
    private Label lblGenFile;
    @FXML
    private Pane pnlHome,pnlOthers;
    @FXML
    private Button btnHome; 
    @FXML
    private PieChart pieChart;
    
    @FXML
    private BarChart barChart;
    
    @FXML
    private ComboBox cmbAlgorithm,cmbKeySize,cmbFile;
    @FXML
    private Button btnOther;
   
    
    
    
    @FXML
    private void HomeClick(ActionEvent event)
    {
        pnlHome.toFront(); 
        
        
    }
    
    @FXML
    private void OtherClick(ActionEvent event)
    {
        pnlOthers.toFront();    
    }
    
 
    
    @Override
    public void initialize(URL url, ResourceBundle rb) {
        cmbFile.getItems().removeAll(cmbFile.getItems());
        cmbFile.getItems().addAll("1Kb", "10Kb", "100Kb","1000Kb");
        
        cmbAlgorithm.getItems().removeAll(cmbAlgorithm.getItems());
        cmbAlgorithm.getItems().addAll("AES", "DES", "RSA");
        
        cmbKeySize.getItems().removeAll(cmbKeySize.getItems());
        cmbKeySize.getItems().addAll("128", "192", "256");
    }    

    @FXML
    private void ExitScene(MouseEvent event) {
        System.exit(0);
    }

    @FXML
    private void FileGenerate(ActionEvent event) throws IOException {
        if((String)cmbFile.getValue() == "1Kb")
        {
            File file = new File("textfile.txt");
            file.createNewFile();
            PrintWriter writer = new PrintWriter(file,"UTF-8");
            Random random = new Random();
            for(int i=0;i<1024;i++)
            {
                writer.print((char)('a'+random.nextInt(26)));
                if(i%8 == 0) 
                {
                    writer.print(" ");
                    i++;
                }
            }
            writer.close();
            lblGenFile.setText("1Kb File Generated");
        }
        else if((String)cmbFile.getValue() == "10Kb")
        {
            File file = new File("textfile.txt");
            file.createNewFile();
            PrintWriter writer = new PrintWriter(file,"UTF-8");
            Random random = new Random();
            for(int i=0;i<1024*10;i++)
            {
                writer.print((char)('a'+random.nextInt(26)));
                if(i%8 == 0) 
                {
                    writer.print(" ");
                    i++;
                }
            }
            writer.close();
            lblGenFile.setText("10Kb File Generated");
        }
        else if((String)cmbFile.getValue() == "100Kb")
        {
            File file = new File("textfile.txt");
            file.createNewFile();
            PrintWriter writer = new PrintWriter(file,"UTF-8");
            Random random = new Random();
            for(int i=0;i<1024*100;i++)
            {
                writer.print((char)('a'+random.nextInt(26)));
                if(i%8 == 0) 
                {
                    writer.print(" ");
                    i++;
                }
            }
            writer.close();
            lblGenFile.setText("100Kb File Generated");
        }
        else
        {
            File file = new File("textfile.txt");
            file.createNewFile();
            PrintWriter writer = new PrintWriter(file,"UTF-8");
            Random random = new Random();
            for(int i=0;i<1024*1000;i++)
            {
                writer.print((char)('a'+random.nextInt(26)));
                if(i%8 == 0) 
                {
                    writer.print(" ");
                    i++;
                }
            }
            writer.close();
            lblGenFile.setText("1000Kb File Generated");
        }
    }

    @FXML
    private void KeySize(ActionEvent event) throws NoSuchAlgorithmException {
        if(AlgorithmModel == "AES")
        {
            encryptionSum = 0;
            decryptionSum = 0;
            encLoadSum = 0;
            decLoadSum = 0;
                   
             

            long startTime = System.nanoTime();
            KeyGenerator keyGenerator = KeyGenerator.getInstance("AES");
            keyGenerator.init(Integer.parseInt(String.valueOf(cmbKeySize.getValue())));
            
            key = keyGenerator.generateKey();

            // Generating IV.
            IV = new byte[16];
            SecureRandom random = new SecureRandom();
            random.nextBytes(IV);
            long endTime = System.nanoTime();
            keyGeneration = (endTime - startTime)/(double)1000;
            
            for(int i=0;i<40;i++) 
            {
                encrypted = EncryptFile();
                decrypted = DecryptFile();
                encryptionTime.add(String.valueOf(AES.encryptionTime));
                decryptionTime.add(String.valueOf(AES.decryptionTime));
                
                encryptionLoad.add(String.valueOf(AES.encryptionCpuLoad));
                decryptionLoad.add(String.valueOf(AES.decryptionCpuLoad));
            }
            
            for(int j=1;j<encryptionTime.size();j++)
            {
                encryptionSum += Double.parseDouble(encryptionTime.get(j));
                decryptionSum += Double.parseDouble(decryptionTime.get(j));
                encLoadSum += Double.parseDouble(encryptionLoad.get(j));
                decLoadSum += Double.parseDouble(decryptionLoad.get(j));

            }
            
            encTime = (float) (encryptionSum/encryptionTime.size()-1);
            decTime = (float) (decryptionSum/encryptionTime.size()-1);
            encLoad = (float) encLoadSum;
            decLoad = (float) decLoadSum;
            
            
            ObservableList<PieChart.Data> pieChartData = 
                FXCollections.observableArrayList(
                    new PieChart.Data("Generate Key", round(keyGeneration,2)),
                    new PieChart.Data("EncryptTime", round(encTime,2)),
                    new PieChart.Data("DecrypTime", round(decTime,2)),
                    new PieChart.Data("EncrypLoad", round(encLoad,2)),
                    new PieChart.Data("DecryptLoad", round(decLoad,2)));
            
            
            
            pieChartData.forEach(data ->
                data.nameProperty().bind(
                        Bindings.concat(
                                data.getName(), " ", data.pieValueProperty(), " µs"
                        )
                )
        );
        pieChart.setLabelsVisible(false);
        pieChart.setLegendSide(Side.BOTTOM);
        
        pieChart.setAnimated(true);
        pieChart.setData(pieChartData);
        
        barChart.getData().clear();
        series.getData().clear();
        
        xAxis.setLabel("Algorithm Type");
        yAxis.setLabel("Time (µs)");
        series.setName("Encryption Time");
        series.getData().add(new XYChart.Data<>("AES",round(encTime,2)));
       barChart.getData().clear();
        barChart.getData().add(series);
        
        }
    }

    @FXML
    private void AlgorithmType(ActionEvent event) {
        AlgorithmModel = (String) cmbAlgorithm.getValue();
    }
    
    
    private File EncryptFile()
    {
        File intpuFile = new File("textfile.txt");
        File outputFile = new File("encryptedfile.txt");
        AES.encrypt(intpuFile, outputFile, key,IV);
        return outputFile;
    }
    private File DecryptFile()
    {
        File intpuFile = new File("encryptedfile.txt");
        File outputFile = new File("decryptedfile.txt"); 
        AES.decrypt(intpuFile, outputFile, key,IV);
        return outputFile;

    }

    private double round(double value,int places) {
        if (places < 0) throw new IllegalArgumentException();

        long factor = (long) Math.pow(10, places);
        value = value * factor;
        long tmp = Math.round(value);
        return (double) tmp / factor;
    }
    
}
