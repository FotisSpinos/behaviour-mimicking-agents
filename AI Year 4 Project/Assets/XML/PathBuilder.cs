using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBuilder
{
    public enum FileTypes
    { 
        ANIMATION_DATA,
        OBJ_RECORD_DATA
    }

    public static string CreateXmlFilePath(FileTypes type, string fileName, string directoryName, bool displayTime)
    {
        string directory = CreateDirectoryPath(type, directoryName);
        string xmlFileName = CreateXmlFileName(fileName, displayTime);


        return directory + "/" + xmlFileName;
    }

    public static string CreateXmlFileName(string fileName, bool displayTime)
    {
        if (!displayTime)
            return fileName + ".xml";
        
        return fileName + "-" + System.DateTime.Now.ToString("dd-MM-yyyy-HHmm") + ".xml";
    }

    public static string CreateDirectoryPath(FileTypes type, string directoryName)
    {
        // define directory
        string directoryPath = "Unsupported";

        if(type == FileTypes.OBJ_RECORD_DATA)
        {
            if (EnvironmentController.GetInstance().IsTraining())
            {
                directoryPath = AnimationDirectories.TRAINING_DATA_DIR;
            }
            else
            {
                directoryPath = AnimationDirectories.AGENT_LAUNCH_DATA_DIR; 
            }
            string description = EnvironmentController.GetInstance().GetDescription();
            directoryPath += description;
        }
        else if(type == FileTypes.ANIMATION_DATA)
        {
            directoryPath = AnimationDirectories.ANIMATIONS_DIR;
        }

        return Application.dataPath + "/Data/" + directoryPath + "/" + directoryName;
    }
}
