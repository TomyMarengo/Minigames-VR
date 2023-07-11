using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;


public class Clock : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
		Transform clock_seconds = transform.GetChild(3);
		Transform clock_minutes = transform.GetChild(2);
		Transform clock_hours = transform.GetChild(1);

		DateTime dtValue = DateTime.Now;
		DateTime first = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
		TimeSpan timeDifference = dtValue.Subtract(first);

		clock_hours.transform.Rotate((float)timeDifference.TotalSeconds/120f,0,0,Space.Self);
		clock_minutes.transform.Rotate((float)timeDifference.TotalSeconds/10f,0,0,Space.Self);
		clock_seconds.transform.Rotate((float)timeDifference.TotalSeconds*6f,0,0,Space.Self);
		
    }

    // Update is called once per frame
    void Update()
{
		Transform clock_seconds = transform.GetChild(3);
		Transform clock_minutes = transform.GetChild(2);
		Transform clock_hours = transform.GetChild(1);

		float secondsRotationSpeed = 6/1f;
		float minutesRotationSpeed = 1/10f;
		float hoursRotationSpeed = 1/120f;

		clock_seconds.transform.Rotate(new Vector3(1,0,0), secondsRotationSpeed * Time.deltaTime);
		clock_minutes.transform.Rotate(new Vector3(1,0,0), minutesRotationSpeed * Time.deltaTime);
		clock_hours.transform.Rotate(new Vector3(1,0,0), hoursRotationSpeed * Time.deltaTime);
		
    }
}