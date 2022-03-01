using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif
using UnityEngine;

public class AndroidNotificationHandler : MonoBehaviour
{
#if UNITY_ANDROID
    const string ChannelId = "notification_channel";

    public void ScheduleNotification(DateTime dateTime)
    {
        AndroidNotificationChannel notificationChannel = new AndroidNotificationChannel
        {
            Id = ChannelId,
            Name = "Notification Channel",
            Description = " Some random desc",
            Importance = Importance.Default
        };

        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

        AndroidNotification notigication = new AndroidNotification
        {
            Title = "Energy Recharged!",
            Text = "Come back to PLAY!",
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = dateTime
        };

        AndroidNotificationCenter.SendNotification(notigication, ChannelId);
    }
#endif
}
