using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Задача скрипта:
///     визуальзировать перемещение и вращение объекта по включению триггера
/// </summary>
public class MoveObject : MonoBehaviour
{
    #region UNITYOBJECTS
    
    [SerializeField] private GameObject ProcessingOfIndications;

    #endregion

    #region DATAfromPLATA

    float[,] GyroAngle;
    float[,] AccelData;
    #endregion

    #region TRIGGERS
    public bool trigger_for_inicialization=false;
    public bool trigger_for_demonstration=false;
    public bool trigger_for_demonstration_by_step = false;
    #endregion

    #region NUMERICAL_VARIABLS
    [SerializeField] private int pointer;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        pointer = 0;
        ProcessingOfIndications = GameObject.FindGameObjectWithTag("processdata");
    }

    // Update is called once per frame
    void Update()
    {
        if(trigger_for_inicialization)
        {
            InicializeData();
        }
    }

    private void FixedUpdate()
    {
        if(trigger_for_demonstration)
        {
            Demonstrations();
        }

        if(trigger_for_demonstration_by_step)
        {
            pointer++;
            trigger_for_demonstration_by_step = false;
            DemonstrationByPoints(pointer);
        }
    }

    void InicializeData()
    {
        GyroAngle = ProcessingOfIndications.GetComponent<ProcessingOfIndications>().GetGyroAngle();
        AccelData = ProcessingOfIndications.GetComponent<ProcessingOfIndications>().GetAccelData();
        Debug.Log("Inicialization DONE");
        trigger_for_inicialization = false;
    }

    void Demonstrations()
    {
        transform.SetPositionAndRotation(transform.position, Quaternion.Euler(GyroAngle[pointer, 0] * 180 / Mathf.PI, GyroAngle[pointer, 1] * 180 / Mathf.PI, GyroAngle[pointer, 2] * 180 / Mathf.PI));
        pointer++;
        if (pointer > (ProcessingOfIndications.GetComponent<ProcessingOfIndications>().GetCountDataInFile()/5)-1)
        {
            pointer = 0;
            trigger_for_demonstration = false;
            Debug.Log("Demonstration DONE");
        }

    }

    void DemonstrationByPoints(int p)
    {
        //transform.SetPositionAndRotation(transform.position, Quaternion.Euler(GyroAngle[p, 0] * 180 / Mathf.PI, GyroAngle[p, 1] * 180 / Mathf.PI, GyroAngle[p, 2] * 180 / Mathf.PI));

        transform.rotation = Quaternion.Lerp(Quaternion.Euler(GyroAngle[p - 1, 0] * 180 / Mathf.PI, GyroAngle[p - 1, 1] * 180 / Mathf.PI, GyroAngle[p - 1, 2] * 180 / Mathf.PI),
                                                Quaternion.Euler(GyroAngle[p, 0] * 180 / Mathf.PI, GyroAngle[p, 1] * 180 / Mathf.PI, GyroAngle[p, 2] * 180 / Mathf.PI)
                                                ,0.2f);
    }

    /// <summary>
    /// Отображение данных акселерометра
    /// Пояснение: Считается иначе, требуется доработка.
    /// </summary>
    void DemonstrationMove()
    {
        transform.SetPositionAndRotation(new Vector3(AccelData[pointer,0]/1000, AccelData[pointer, 1]/1000, AccelData[pointer, 2]/1000), Quaternion.identity);
        pointer++;
        if (pointer > (ProcessingOfIndications.GetComponent<ProcessingOfIndications>().GetCountDataInFile() / 5) - 1)
        {
            pointer = 0;
            trigger_for_demonstration_by_step = false;
            Debug.Log("Demonstration move DONE");
        }
    }
}
