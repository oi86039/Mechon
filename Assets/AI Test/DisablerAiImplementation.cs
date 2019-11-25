using System.Collections.Generic;
using UnityEngine;
using DisablerAi;

namespace DisablerAi_Implemented
{
    /**
     * @brief Represents the position of the player's gun.
     */
    public class Disabler
    {
        public Disabler(Location Location)
        {
            this.Location = Location;
        }

        public Location Location { get; set; }    /**Current Location of this gun*/
    }

    /**
   * @brief Represents the position and marked status of an important item in the game world (ammo or weapon).
   */
    public class Item
    {
        public Item(Location Location)
        {
            this.Location = (Location)(Location);
        }

        public Location Location { get; set; }  /**Current Location of this item*/

        public void MarkForPlayer()
        {
            throw new System.NotImplementedException();
        }
    }

    /**
     * @brief Represents the position of something. (Vector3 in other words)
     */
    public class Location
    {
        public Vector3 Position { get; set; }  //Location in real world space

        public Location(Vector3 vector3)
        {
            Position = vector3;
        } //Constructor

        //Calculate distance between locations
        public float DistanceFrom(Location location) { return Vector3.Distance(Position, location.Position); }

        public Location RandomLocation(float distanceFromPlayer, float distanceFromRobots) { throw new System.NotImplementedException(); }
    }

    /**
     * @brief Represents the Player and anything near its position.
     */
    public class Player
    {
        public Disabler Disabler { get; set; }
        public Location Location { get; set; } /**Current Location of the Player*/

        public Player(Disabler disabler, Location location)
        {
            this.Disabler = disabler;
            this.Location = location;
        }

        public List<Item> NearestItems()
        {
            throw new System.NotImplementedException();
        }

        public List<Robot> NearestRobots()
        {
            throw new System.NotImplementedException();
        }
    }

    /**
     * @brief Represents the common properties of a robot enemy, including health, Line of Sight (LOS), and more
     */
    public class Robot
    {
        GameObject gameObject; /**Robot's gameobject*/
        /**Player's gameobject*/

        //Place constructor here
        public Robot(Location location, Location target, Location patrolStart, Location patrolEnd, List<Location> pointsOfInterest, RobotHead head, int health)
        {
            this.Location = location;
            this.Target = target;
            this.PatrolStart = patrolStart;
            this.PatrolEnd = patrolEnd;

            //Cast Locations to ILocations in list, then assign
            foreach (Location PoI in pointsOfInterest)
            {
                this.PointsOfInterest.Add(PoI);
            }

            PlayingAnimation = RobotAnimation.None; //Start as stationary
            this.Head = head;

            //Default values
            DetectionLineOfSight = false;
            DetectionAudio = false;
            Shot = false;
            HitWithItem = false;
            this.Health = Health;


        }


        public Location Location { get; set; }                                 /**Current Location of this robot*/
        public Location Target { get; set; }                                          /**Current Location that the robot is walking towards*/
        public Location PatrolStart { get; set; }                           /**Start of starting patrol path*/
        public Location PatrolEnd { get; set; }                              /**Position of starting patrol path*/
        public List<Location> PointsOfInterest { get; set; }      /**List of different points for the AI to go to.*/
        public RobotAnimation PlayingAnimation { get; set; }    /**Check what animation is currently playing*/
        public bool DetectionLineOfSight { get; set; }   /**Detect if player is seen or not*/
        public bool DetectionAudio { get; set; }                /**Detect if player is heard or not*/
        public bool Shot { get; set; }                                           /**Detect if robot was hurt with a shot or not*/
        public bool HitWithItem { get; set; }                              /**Detect if robot was hurt with a thrown item or not*/

        public RobotHead Head { get; set; }                                              /**Robot's head for headshots*/

        public int Health { get; set; }                                        /**Robot's health. If 0, switch to disabled state*/

        public bool CanHear(Player player) //Implement Johanne's LOS code here
        {
            //To test
            return false;
        }

        public bool CanSee(Player player) //Implement Johanne's LOS code here
        {
            //To test
            return true;
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
    public class RobotHead
    {
        public Location Location { get; set; }
        public bool Shot { get; set; }

        public RobotHead(Location location)
        {
            this.Location = location;
            Shot = false;
        }
    }

}