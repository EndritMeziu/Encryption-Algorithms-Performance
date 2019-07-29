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
import java.security.KeyPair;
import java.security.KeyPairGenerator;
import java.security.NoSuchAlgorithmException;
import java.security.PrivateKey;
import java.security.PublicKey;
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
import javax.crypto.spec.*;
import java.security.*;
import javax.crypto.*;

/**
 *
 * @author USER
 */
public class FXMLDocumentController implements Initializable {
    
    byte[] IV;
    SecretKey key;
    PublicKey publicKey;
    PrivateKey privateKey;
    File encrypted;
    File decrypted;
    double encryptionSum;
    double decryptionSum;
    double encLoadSum;
    double decLoadSum;
    double keyGeneration;
    
    boolean onceaes = true;
    boolean oncedes = true;
    boolean oncersa = true;
    boolean oncetripledes = true;
    boolean oncerc4 = true;

    ArrayList<String> encryptionTime = new ArrayList<String>(); 
    ArrayList<String> decryptionTime = new ArrayList<String>(); 
    ArrayList<String> encryptionLoad = new ArrayList<String>(); 
    ArrayList<String> decryptionLoad = new ArrayList<String>();
    XYChart.Series series1;
    XYChart.Series series;
    XYChart.Series series2;
    XYChart.Series series3;
    XYChart.Series series4;
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
        cmbAlgorithm.getItems().addAll("AES", "DES", "Blowfish","3DES","RC4");
        
        series = new XYChart.Series();
        series.setName("DES");
        
        series1 = new XYChart.Series();
        series1.setName("AES");
        
        series2 = new XYChart.Series();
        series2.setName("Blowfish");
        
        series3 = new XYChart.Series();
        series3.setName("3DES");
        
        series4 = new XYChart.Series();
        series4.setName("IDEA");
        
    
        barChart.getData().add(series1);
        barChart.getData().add(series);
        barChart.getData().add(series2);
        barChart.getData().add(series3);
        barChart.getData().add(series4);

       
        
        
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
            if(cmbKeySize.getValue().equals("") || cmbKeySize.getValue().equals(null) ||
                    cmbKeySize.getValue() == null || cmbKeySize.getValue() == ""){
                keyGenerator.init(128);
            }
            System.out.print(cmbKeySize.getValue());
            keyGenerator.init(Integer.parseInt(String.valueOf(cmbKeySize.getValue())));
            
            key = keyGenerator.generateKey();
            
            
            long endTime = System.nanoTime();
            keyGeneration = (endTime - startTime)/(double)1000;
            encryptionTime.clear();
            decryptionTime.clear();
            encryptionLoad.clear();
            decryptionLoad.clear();
            for(int i=0;i<20;i++) 
            {
                encrypted = AESEncryptFile();
                decrypted = AESDecryptFile();
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
        
        if(onceaes == true){
            
            onceaes = false;
            System.out.println("hyri");
        }
        yAxis.setLabel("Time (µs)");
        
        series1.getData().add(new XYChart.Data<>("",round(encTime,2)));
        
       
        
        }
        else if(AlgorithmModel == "DES")
        {
            encryptionSum = 0;
            decryptionSum = 0;
            encLoadSum = 0;
            decLoadSum = 0;
            System.out.println("ne unaze");
             

            long startTime = System.nanoTime();
            KeyGenerator keyGenerator = KeyGenerator.getInstance("DES");
            keyGenerator.init(56);
            
            key = keyGenerator.generateKey();

            
            long endTime = System.nanoTime();
            keyGeneration = (endTime - startTime)/(double)1000;
            encryptionTime.clear();
            decryptionTime.clear();
            encryptionLoad.clear();
            decryptionLoad.clear();
    
            for(int i=0;i<20;i++) 
            {
                encrypted = DESEncryptFile();
                decrypted = DESDecryptFile();
                encryptionTime.add(String.valueOf(DES.encryptionTime));
                decryptionTime.add(String.valueOf(DES.decryptionTime));
                
                encryptionLoad.add(String.valueOf(DES.encryptionCpuLoad));
                decryptionLoad.add(String.valueOf(DES.decryptionCpuLoad));
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
        
      
        if(oncedes){
            
            oncedes = false;
        }
        yAxis.setLabel("Time (µs)");
        

        series.getData().add(new XYChart.Data<>("",round(encTime,2)));
      
        }
        else if(AlgorithmModel == "Blowfish")
        {
            encryptionSum = 0;
            decryptionSum = 0;
            encLoadSum = 0;
            decLoadSum = 0;
            System.out.println("ne unaze");
             

            long startTime = System.nanoTime();
            KeyGenerator keyGenerator = KeyGenerator.getInstance("Blowfish");
            
            if(cmbKeySize.getValue().equals("") || cmbKeySize.getValue().equals(null) ||
                    cmbKeySize.getValue() == null || cmbKeySize.getValue() == ""){
                keyGenerator.init(32);
            }
            System.out.print(cmbKeySize.getValue());
            keyGenerator.init(Integer.parseInt(String.valueOf(cmbKeySize.getValue())));
            
            key = keyGenerator.generateKey();

          
            long endTime = System.nanoTime();
            keyGeneration = (endTime - startTime)/(double)1000;
            
            encryptionTime.clear();
            decryptionTime.clear();
            encryptionLoad.clear();
            decryptionLoad.clear();
            
            for(int i=0;i<20;i++) 
            {
                encrypted = BlowfishEncryptFile();
                decrypted = BlowfishDecryptFile();
                encryptionTime.add(String.valueOf(Blowfish.encryptionTime));
                decryptionTime.add(String.valueOf(Blowfish.decryptionTime));
                
                encryptionLoad.add(String.valueOf(Blowfish.encryptionCpuLoad));
                decryptionLoad.add(String.valueOf(Blowfish.decryptionCpuLoad));
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
        
      
        
        yAxis.setLabel("Time (µs)");
        

        series2.getData().add(new XYChart.Data<>("",round(encTime,2)));
        }
        else if(AlgorithmModel == "3DES")
        {
            encryptionSum = 0;
            decryptionSum = 0;
            encLoadSum = 0;
            decLoadSum = 0;
            System.out.println("ne unaze");
             

            long startTime = System.nanoTime();
            KeyGenerator keyGenerator = KeyGenerator.getInstance("DESede");
            if(cmbKeySize.getValue().equals("") || cmbKeySize.getValue().equals(null) ||
                    cmbKeySize.getValue() == null || cmbKeySize.getValue() == ""){
                keyGenerator.init(112);
            }
            System.out.print(cmbKeySize.getValue());
            keyGenerator.init(Integer.parseInt(String.valueOf(cmbKeySize.getValue())));
            
            key = keyGenerator.generateKey();

         
            
            long endTime = System.nanoTime();
            keyGeneration = (endTime - startTime)/(double)1000;
            encryptionTime.clear();
            decryptionTime.clear();
            encryptionLoad.clear();
            decryptionLoad.clear();
    
            for(int i=0;i<20;i++) 
            {
                encrypted = TripleDESEncryptFile();
                decrypted = TripleDESDecryptFile();
                encryptionTime.add(String.valueOf(TripleDES.encryptionTime));
                decryptionTime.add(String.valueOf(TripleDES.decryptionTime));
                
                encryptionLoad.add(String.valueOf(TripleDES.encryptionCpuLoad));
                decryptionLoad.add(String.valueOf(TripleDES.decryptionCpuLoad));
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
        
      
        if(oncetripledes){
            
            oncetripledes = false;
        }
        yAxis.setLabel("Time (µs)");
        

        series3.getData().add(new XYChart.Data<>("",round(encTime,2)));
        }
        else if(AlgorithmModel == "RC4")
        {
            encryptionSum = 0;
            decryptionSum = 0;
            encLoadSum = 0;
            decLoadSum = 0;
            System.out.println("ne unaze");
             

            long startTime = System.nanoTime();
            KeyGenerator keyGenerator = KeyGenerator.getInstance("RC4");
            if(cmbKeySize.getValue().equals("") || cmbKeySize.getValue().equals(null) ||
                    cmbKeySize.getValue() == null || cmbKeySize.getValue() == ""){
                keyGenerator.init(512);
            }
            System.out.print(cmbKeySize.getValue());
            keyGenerator.init(Integer.parseInt(String.valueOf(cmbKeySize.getValue())));
            
            key = keyGenerator.generateKey();

         
            
            long endTime = System.nanoTime();
            keyGeneration = (endTime - startTime)/(double)1000;
            encryptionTime.clear();
            decryptionTime.clear();
            encryptionLoad.clear();
            decryptionLoad.clear();
    
            for(int i=0;i<20;i++) 
            {
                encrypted = RC4EncryptFile();
                decrypted = RC4DecryptFile();
                encryptionTime.add(String.valueOf(RC4.encryptionTime));
                decryptionTime.add(String.valueOf(RC4.decryptionTime));
                
                encryptionLoad.add(String.valueOf(RC4.encryptionCpuLoad));
                decryptionLoad.add(String.valueOf(RC4.decryptionCpuLoad));
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
        
      
        if(oncerc4){
            
            oncerc4 = false;
        }
        yAxis.setLabel("Time (µs)");
        

        series4.getData().add(new XYChart.Data<>("",round(encTime,2)));
        }
        
        
    }

    @FXML
    private void AlgorithmType(ActionEvent event) throws NoSuchAlgorithmException {
        AlgorithmModel = (String) cmbAlgorithm.getValue();
        if(AlgorithmModel == "DES")
        {
            cmbKeySize.getItems().removeAll(cmbKeySize.getItems());
            cmbKeySize.getItems().addAll("56");
            
        }
        else if(AlgorithmModel == "AES")
        {
            cmbKeySize.getItems().removeAll(cmbKeySize.getItems());
            cmbKeySize.getItems().addAll("128", "192", "256");
        }
        else if(AlgorithmModel == "Blowfish")
        {
            cmbKeySize.getItems().removeAll(cmbKeySize.getItems());
            cmbKeySize.getItems().addAll("32", "128", "256","448");
        }
        else if(AlgorithmModel == "3DES")
        {
            cmbKeySize.getItems().removeAll(cmbKeySize.getItems());
            cmbKeySize.getItems().addAll("112","168");
        }
        else if(AlgorithmModel == "RC4")
        {
            cmbKeySize.getItems().removeAll(cmbKeySize.getItems());
            cmbKeySize.getItems().addAll("128","256","1024");
        }
       
        
    }
    
    
    private File AESEncryptFile()
    {
        File intpuFile = new File("textfile.txt");
        File outputFile = new File("encryptedfile.txt");
        AES.encrypt(intpuFile, outputFile, key);
        return outputFile;
    }
    private File AESDecryptFile()
    {
        File intpuFile = new File("encryptedfile.txt");
        File outputFile = new File("decryptedfile.txt"); 
        AES.decrypt(intpuFile, outputFile, key);
        return outputFile;

    }
    
    private File DESEncryptFile()
    {
        File intpuFile = new File("textfile.txt");
        File outputFile = new File("encryptedfile.txt");
        DES.encrypt(intpuFile, outputFile, key);
        return outputFile;
    }
    private File DESDecryptFile()
    {
        File intpuFile = new File("encryptedfile.txt");
        File outputFile = new File("decryptedfile.txt"); 
        DES.decrypt(intpuFile, outputFile, key);
        return outputFile;

    }
    
    private File BlowfishEncryptFile()
    {
        File intputFile = new File("textfile.txt");
        File outputFile = new File("encryptedfile.txt");
        Blowfish.encrypt(intputFile, outputFile, key);
        return outputFile;
    }
    private File BlowfishDecryptFile()
    {
        File intputFile = new File("encryptedfile.txt");
        File outputFile = new File("decryptedfile.txt"); 
        Blowfish.decrypt(intputFile, outputFile, key);
        return outputFile;

    }
    
    private File TripleDESEncryptFile()
    {
        File intputFile = new File("textfile.txt");
        File outputFile = new File("encryptedfile.txt");
        TripleDES.encrypt(intputFile, outputFile, key);
        return outputFile;
    }
    private File TripleDESDecryptFile()
    {
        File intputFile = new File("encryptedfile.txt");
        File outputFile = new File("decryptedfile.txt"); 
        TripleDES.decrypt(intputFile, outputFile, key);
        return outputFile;

    }
    private File RC4EncryptFile()
    {
        File intputFile = new File("textfile.txt");
        File outputFile = new File("encryptedfile.txt");
        RC4.encrypt(intputFile, outputFile, key);
        return outputFile;
    }
    private File RC4DecryptFile()
    {
        File intputFile = new File("encryptedfile.txt");
        File outputFile = new File("decryptedfile.txt"); 
        RC4.decrypt(intputFile, outputFile, key);
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
