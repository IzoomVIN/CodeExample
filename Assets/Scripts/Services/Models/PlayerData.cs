using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;

namespace Services.Models
{
    public class PlayerData
    {
        public event ChangePointsDelegate ChangePointsEvent;
        public delegate void ChangePointsDelegate(int points);


        public UnityEvent<float> ChangeHpEvent;
        
        public int Points { get; private set; }

        public PlayerData()
        {
            Points = 0;
        }

        public void AddPoints(int value)
        {
            Points += value;
            ChangePointsEvent?.Invoke(Points);
        }

        public void ChangeHp(float value)
        {
            ChangeHpEvent?.Invoke(value);
        }
    }
}