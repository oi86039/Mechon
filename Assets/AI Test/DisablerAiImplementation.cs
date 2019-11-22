using System.Collections;
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
        public Disabler(Location Location)
        {
            this.Location = Location;
        }

        public ILocation Location { get; set; }    /**Current Location of this gun*/
    }

    /**
   * @brief Represents the position and marked status of an important item in the game world (ammo or weapon).
   */
    public class Item : IItem
    {
        public Item(Location Location)
        {
            this.Location = (ILocation)(Location);
        }

        public ILocation Location { get; set; }  /**Current Location of this item*/

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
        public Vector3 Position { get; set; }  //Location in real world space

        public Location(Vector3 vector3)
        {
            Position = vector3;
        } //Constructor

        //Calculate distance between locations
        public float DistanceFrom(ILocation location) { return Vector3.Distance(Position, ((Location)location).Position); }

        public ILocation RandomLocation(float distanceFromPlayer, float distanceFromRobots) { throw new System.NotImplementedException(); }
    }

    /**
     * @brief Represents the Player and anything near its position.
     */
    public class Player : IPlayer
    {
        public IDisabler Disabler { get { return Disabler; } set { Disabler = value; } }
        public ILocation Location { get { return Location; } set { Location = value; } }  /**Current Location of the Player*/

        public Player(IDisabler Disabler, Location Location)
        {
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
        public Robot(Location Location, Location Target, Location PatrolStart, Location PatrolEnd, List<Location> PointsOfInterest, IRobotHead Head, int Health)
        {
            this.Location = Location;
            this.Target = Target;
            this.PatrolStart = PatrolStart;
            this.PatrolEnd = PatrolEnd;

            //Cast Locations to ILocations in list, then assign
            foreach (Location PoI in PointsOfInterest)
            {
                this.PointsOfInterest.Add((ILocation)(PoI));
            }

            PlayingAnimation = RobotAnimation.None; //Start as stationary
            this.Head = Head;

            //Default values
            DetectionLineOfSight = false;
            DetectionAudio = false;
            Shot = false;
            HitWithItem = false;
            this.Health = Health;


        }


        public ILocation Location { get; set; }                                 /**Current Location of this robot*/
        public ILocation Target { get; set; }                                          /**Current Location that the robot is walking towards*/
        public ILocation PatrolStart { get; set; }                           /**Start of starting patrol path*/
        public ILocation PatrolEnd { get; set; }                              /**Position of starting patrol path*/
        public List<ILocation> PointsOfInterest { get; set; }      /**List of different points for the AI to go to.*/
        public RobotAnimation PlayingAnimation { get; set; }    /**Check what animation is currently playing*/
        public bool DetectionLineOfSight { get; set; }   /**Detect if player is seen or not*/
        public bool DetectionAudio { get; set; }                /**Detect if player is heard or not*/
        public bool Shot { get; set; }                                           /**Detect if robot was hurt with a shot or not*/
        public bool HitWithItem { get; set; }                              /**Detect if robot was hurt with a thrown item or not*/

        public IRobotHead Head { get; set; }                                              /**Robot's head for headshots*/

        public int Health { get; set; }                                        /**Robot's health. If 0, switch to disabled state*/

        public bool CanHear(IPlayer player) //Implement Johanne's LOS code here
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
        public ILocation Location { get; set; }
        public bool Shot { get; set; }

        public RobotHead(ILocation Location)
        {
            this.Location = Location;
            Shot = false;
        }
    }

}