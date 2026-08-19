using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
public class SensorTelemetry : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statisticsText;
    [SerializeField] private TextMeshProUGUI statisticsText2;
    private UnityEngine.InputSystem.Gyroscope gyroscope;

    private void Start()
    {
        if (AttitudeSensor.current != null) InputSystem.EnableDevice(AttitudeSensor.current);


        if (UnityEngine.InputSystem.Gyroscope.current != null)
        {
            gyroscope = UnityEngine.InputSystem.Gyroscope.current;
            InputSystem.EnableDevice(gyroscope);
        }
    }

    private void Update()
    {

        string textOutput = "null";

        if (AttitudeSensor.current != null && AttitudeSensor.current.enabled)
        {
            Vector3 inclination = AttitudeSensor.current.attitude.ReadValue().eulerAngles;
            textOutput = "X = " + inclination.x.ToString() + ", Y = " + inclination.y.ToString() + ", Z = " + inclination.z.ToString();
            //textOutput = $"X = {inclination.x}, Y = {inclination.y}, Z = {inclination.z} ";
        }
        statisticsText.text = textOutput;


        string textOutput2 = "null";

        if (gyroscope != null && gyroscope.enabled)
        {
            Vector3 velocity = gyroscope.angularVelocity.ReadValue();

        }
        statisticsText2.text = textOutput2;

    }


}