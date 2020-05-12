using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class UnityCarStateRecorder : AbstractRecorder
{
    private SerCarControllerAtr data;
    private CarController carController;

    public UnityCarStateRecorder(UnityVehicle car, float fixedTimeInterval)
    {
        this.carController = car.CarController;
        data = new SerCarControllerAtr();

        data.timeInterval = fixedTimeInterval;
    }

    public override void ClearRecordedData()
    {
        data.carContrAtr.Clear();
    }

    public override void StoreImpactPoints(string directoryName, string fileName, PathBuilder.FileTypes fileType)
    {
        throw new System.NotImplementedException();
    }

    public override void StoreStates(string directoryName, string fileName, PathBuilder.FileTypes fileType)
    {
        isRecording = false;

        string path = PathBuilder.CreateXmlFilePath(fileType,
            directoryName,
            fileName,
            false);

        XmlReadWrite.GetInstance().SaveCarControllerAtributes(path, data);
    }

    protected override void RecordState()
    {
        // PASS BY REFERENCR
        data.carContrAtr.Add(carController.GetCarControllerAtr()); //carController.GetCarControllerA        
        Debug.Log("Local Position: " + carController.GetCarControllerAtr().localPosition);
        Debug.Log("Local Euler Angles: " + carController.GetCarControllerAtr().localEulerAngles);
        Debug.Log("Local Velocity: " + carController.GetCarControllerAtr().velocity);
    }
}
