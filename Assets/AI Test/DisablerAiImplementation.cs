﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DisablerAi;
using DisablerAi.Interfaces;

namespace DisablerAi_Implemented
{
    /**
     * @brief Represents the position of the player's gun.
     */
    public class Disabler : IDisabler
    {
        public ILocation Location { get { return Location; } set { Location = value; } }  /**Current Location of this gun*/
    }

    /**
   * @brief Represents the position and marked status of an important item in the game world (ammo or weapon).
   */
    public class Item : IItem
    {
        public ILocation Location { get { return Location; } set { Location = value; } }  /**Current Location of this item*/

        public void MarkForPlayer()
        {
            throw new System.NotImplementedException();
        }
    }

    /**
     * @brief Represents the position of something. (Vector3 in other words)
     */
    public class Location : ILocation
    {
        public Vector3 position; //Location in real world space

        public Location(Vector3 vector3) { position = vector3; } //Constructor

        public Vector3 GetPosition() { return position; }
        public void SetPosition(Vector3 vector3) { position = vector3; }
       
        //Calculate distance between locations
        public float DistanceFrom(ILocation location) { return Vector3.Distance(position, ((Location)location).GetPosition()); }

        public ILocation RandomLocation(float distanceFromPlayer, float distanceFromRobots) { throw new System.NotImplementedException(); }
    }

    /**
     * @brief Represents the Player and anything near its position.
     */
    public class Player : IPlayer
    {
        public IDisabler Disabler { get { return Disabler; } set { Disabler = value; } }
        public ILocation Location { get { return Location; } set { Location = value; } }  /**Current Location of the Player*/

        public Player(IDisabler Disabler, ILocation Location) {
            this.Disabler = Disabler;
            this.Location = Location;
        }

        public List<IItem> NearestItems()
        {
            throw new System.NotImplementedException();
        }

        public List<IRobot> NearestRobots()
        {
            throw new System.NotImplementedException();
        }
    }

    /**
     * @brief Represents the common properties of a robot enemy, including health, Line of Sight (LOS), and more
     */
    public class Robot : IRobot
    {
        GameObject gameObject; /**Robot's gameobject*/
        /**Player's gameobject*/

        //Place constructor here
        public Robot(ILocation Location, ILocation Target, ILocation PatrolStart, ILocation PatrolEnd, List<ILocation> PointsOfInterest,RobotAnimation PlayingAnimation, IRobotHead Head) {
            this.Location = Location;
            this.Target = Target;
            this.PatrolStart = PatrolStart;
            this.
        }


        public ILocation Location { get { return Location; } set { Location = value; } }                                  /**Current Location of this robot*/
        public ILocation Target { get { return Target; } set { Target = value; } }                                        /**Current Location that the robot is walking towards*/
        public ILocation PatrolStart { get { return PatrolStart; } set { PatrolStart = value; } }                         /**Start of starting patrol path*/
        public ILocation PatrolEnd { get { return PatrolEnd; } set { PatrolStart = value; } }                             /**Position of starting patrol path*/
        public List<ILocation> PointsOfInterest { get { return PointsOfInterest; } set { PointsOfInterest = value; } }    /**List of different points for the AI to go to.*/
        public RobotAnimation PlayingAnimation { get { return PlayingAnimation; } set { PlayingAnimation = value; } }    /**Check what animation is currently playing*/
        public bool DetectionLineOfSight { get { return DetectionLineOfSight; } set { DetectionLineOfSight = value; } }  /**Detect if player is seen or not*/
        public bool DetectionAudio { get { return DetectionAudio; } set { DetectionAudio = value; } }                    /**Detect if player is heard or not*/
        public bool Shot { get { return Shot; } set { Shot = value; } }                                                  /**Detect if robot was hurt with a shot or not*/
        public bool HitWithItem { get { return HitWithItem; } set { HitWithItem = value; } }                             /**Detect if robot was hurt with a thrown item or not*/

        public IRobotHead Head { get { return Head; } set { Head = value; } }                                            /**Robot's head for headshots*/

        public int Health { get { return Health; } set { Health = value; } }                                             /**Robot's health. If 0, switch to disabled state*/

        public bool CanHear(IPlayer player)
        {
            throw new System.NotImplementedException();
        }

        public bool CanSee(IPlayer player) //Implement Johanne's LOS code here
        {
            throw new System.NotImplementedException();
        }

        public void MarkForPlayer()
        {
            throw new System.NotImplementedException();
        }

        public bool ReachedTarget(float distanceForgiveness = 0.5F)
        {
            throw new System.NotImplementedException();
        }
        /**GameObject this class is attached to*/
    }

    /**
     * @brief Represents the Robot's head for headshots and the like
     */
    public class RobotHead : IRobotHead
    {
        public ILocation Location { get { return Location; } set { Location = value; } }
        public bool Shot { get { return Shot; } set { Shot = value; } }

        public RobotHead(ILocation Location) {
            this.Location = Location;
            Shot = false;
        }
    }

}