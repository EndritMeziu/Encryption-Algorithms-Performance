/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package encryptionalgorithmsperformance;

/**
 *
 * @author USER
 */
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.lang.management.ManagementFactory;
import java.lang.management.ThreadMXBean;
import java.security.AccessControlException;
import javax.crypto.Cipher;
import javax.crypto.SecretKey;
import javax.crypto.spec.IvParameterSpec;
import javax.crypto.spec.SecretKeySpec;

/**
 *
 * @author USER
 */
public class DES {
    private static SecretKeySpec secretKey;
    private static byte[] key;
    static double encryptionTime;
    static double decryptionTime;
    static double encryptionCpuLoad;
    static double decryptionCpuLoad;
    DES()
    {
    	
    }
 
    
 
    public static void encrypt(File inputFile,File outputFile, SecretKey key)
    {
        
        try
        {
            ThreadMXBean newBean = ManagementFactory.getThreadMXBean();
            if (newBean.isThreadCpuTimeSupported())
            {
            newBean.setThreadCpuTimeEnabled(true);
            }
            else
            {
                throw new AccessControlException("");
            }
            long startThreadTime = newBean.getCurrentThreadCpuTime();
            Cipher cipher = Cipher.getInstance("DES/ECB/PKCS5Padding");
            SecretKeySpec keySpec = new SecretKeySpec(key.getEncoded(),"DES");
            cipher.init(Cipher.ENCRYPT_MODE, keySpec);
            
            FileInputStream inputStream = new FileInputStream(inputFile);
            byte[] inputBytes = new byte[(int) inputFile.length()];
            inputStream.read(inputBytes);
            long startTime = System.nanoTime();

            byte[] outputBytes = cipher.doFinal(inputBytes);


            FileOutputStream outputStream = new FileOutputStream(outputFile);
            outputStream.write(outputBytes);
            long endTime = System.nanoTime();
            inputStream.close();
            outputStream.close();
            long endthreadTime = newBean.getCurrentThreadCpuTime();
            encryptionCpuLoad = (endthreadTime - startThreadTime) / (double)(1000000);
            encryptionTime = (endTime - startTime)/(double)1000;
            
        }
       
        catch (AccessControlException e)
        {
            System.out.println("CPU Usage monitoring is not available!");
            System.exit(0);
        }
        catch (Exception e)
        {
            System.out.println("Error while encrypting: " + e.toString());
        }
    }
 
    public static void decrypt(File inputFile,File outputFile, SecretKey key)	
    {
        try
        {
            ThreadMXBean newBean = ManagementFactory.getThreadMXBean();
            if (newBean.isThreadCpuTimeSupported())
            {
            newBean.setThreadCpuTimeEnabled(true);
            }
            else
            {
                throw new AccessControlException("");
            }
            long startThreadTime = newBean.getCurrentThreadCpuTime();
            Cipher cipher = Cipher.getInstance("DES/ECB/PKCS5Padding");
            SecretKeySpec keySpec = new SecretKeySpec(key.getEncoded(),"DES");

            cipher.init(Cipher.DECRYPT_MODE, keySpec);
            
            FileInputStream inputStream = new FileInputStream(inputFile);
            byte[] inputBytes = new byte[(int) inputFile.length()];
            inputStream.read(inputBytes);
            long startTime = System.nanoTime(); 
            byte[] outputBytes = cipher.doFinal(inputBytes);


            FileOutputStream outputStream = new FileOutputStream(outputFile);
            outputStream.write(outputBytes);
            long endTime = System.nanoTime();
            long endthreadTime = newBean.getCurrentThreadCpuTime()+1;
            decryptionCpuLoad = (endthreadTime - startThreadTime) / (double)(1000000);
            decryptionTime = (endTime - startTime)/(double)1000;
     

        inputStream.close();
        outputStream.close();
            
        }
        catch (Exception e)
        {
            System.out.println("Error while decrypting: " + e.toString());
        }
    }
}