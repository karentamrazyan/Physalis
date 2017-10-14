/*
 * Project Title : inHausOSGiGateway
 * Author        : Lohrasb jalali
 * Company       : Fraunhofer IMS
 */

package inHausLamp;
import de.berkom.ihome.coreservices.notification.lib.NotificationRegister;
import de.berkom.ihome.coreservices.notification.lib.NotificationListener;

//import de.tnova.training.notificationproxy.lib.NotificationListener;
//import de.tnova.training.notificationproxy.lib.NotificationRegister;

/**
 *
 *
 * @author Lohrasb jalali
 * @version
 * @since
 */
public class NotificationRegisterImpl implements NotificationRegister {
private NotificationListener listener = null;
 /**
   *
   *
   * @param param0
   */
  public void addNotificationListener(NotificationListener param0) {
    System.out.println("Neuer Listener registriert!");

    listener = param0;
    //   notifyListener("Hello Telekom");

  }

  /**
   *
   *
   * @param param0
   */
  public void removeNotificationListener(NotificationListener param0) {
    listener = null;
  }

  /**
   *
   *
   * @param msg
   */
  public void notifyListener(String  msg) {
    if(listener!= null)
      {
        listener.push(msg,msg,msg);
      }
  }

}