/*
 * Project Title : inHausOSGiGateway
 * Author        : Lohrasb jalali
 * Company       : Fraunhofer IMS
 */

package de.inhaus.inHausLamp;

/**
 * 
 * 
 * @author Lohrasb jalali
 * @version 
 * @since 
 */
public interface LampEvents {
  
  public void stateChanged(String deviceID,boolean state);
  public void intensityChanged(String deviceID,int intensity);

}
