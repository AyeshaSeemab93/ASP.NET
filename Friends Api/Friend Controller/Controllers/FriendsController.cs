using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;


namespace Friend_Controller.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FriendsController : ControllerBase
    {
        //variable of class -similar to: public string Name;
        public static List<string> Friends = new List<string>()
        {
            "Josi,Mario",
            "Paavo,Juha"
        };


        [HttpGet("GetFriendsList")]
        public IActionResult GetFriendsList()
        {
            return Ok(Friends);
        }


        [HttpGet("GetSpecificFriendList")]

        public IActionResult GetSpecificFriendList(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Invalid friendship data.");
            }
            //got throught the list(checking each element/group)
            foreach (string group in Friends)
            {
                //splitting each group into names, saving 2 or 3 splitted strings so array []
                string[] names = group.Split(',');
                
                    //going through all the parts of group and check if they match
                    for (int x = 0; x < names.Length; x++)//if (names.Any(n =>n == name || n.ToLower() == name ))
                {
                    //checking both Paavo and paavo
                    if (names[x] == name || names[x].ToLower() ==name)
                    {
                        return Ok(group);
                    }
                }

            }
            return NotFound("Friend Not Found in the list");
        }





        [HttpPost("AddFriend")]
        public IActionResult AddFriend(string groupMember, string newFriend)
        {
            if (string.IsNullOrEmpty(newFriend))
            {
                return BadRequest("Invalid friendship data.");
            }
            //going through list
            for (int index = 0; index<Friends.Count; index++)
            {
                string group = Friends[index];
                string[] names = group.Split(',');
                // //(going through string)if any member of names is asked groupMember
                if ( names.Any(name => name == groupMember || name.ToLower() == groupMember))
                {
                    //updating the list with adding name to group
                    Friends[index] += "," + newFriend;

                    return Ok(Friends);
                }
                        //or simple easy way to do above any method

                        //for (int i = 0; i<names.Length; i++)
                        //  {
                        //      if (names[i] == groupMember || names[i].ToLower() == groupMember)
                        //{
                        //    Friends[index] += "," + newFriend;
                        //    return Ok(Friends);
                        //}
                        //}
            }

            return NotFound("Name not found in the list");

        }

        [HttpPost("AddFriendtoAll")]
        public IActionResult AddFriendtoAll(string friend)
        {
            if (string.IsNullOrEmpty(friend))
            {
                return BadRequest("No friendship data found");
            }

            for(int i = 0; i<Friends.Count; i++)
            {
                Friends[i] = Friends[i] + "," + friend;
            }
            return Ok(Friends);
        }

        [HttpPost("AddFriendtoAll2ndWay")]
        public IActionResult AddFriendtoAll2ndWay(string friend)
        {
            //Select method create new elements(group) + add friend to each.......make new list...put back to Friends
            Friends = Friends.Select(group => group + ',' + friend).ToList();
            return Ok(Friends);
        }


        [HttpDelete("DeleteFriendShip")]
        public IActionResult DeleteFriendShip([FromBody]string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("No data entered");
            }

            Friends.RemoveAll(friend => friend.Contains(name));
            return Ok($"Removed friendlist for {name}.");
        }

        [HttpDelete("DeleteFriend")]
        public IActionResult DeleteFriend([FromBody] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("No data entered");
            }

            // Iterate through each entry in the Friends list
            foreach (var friendEntry in Friends.ToList())
            {
                // Split the entry into individual names
                var names = friendEntry.Split(',');

                // Check if the name matches "Juha" and remove it
                if (names.Contains(name))
                {
                    names = names.Where(n => n != name).ToArray();// if the n is not name then add to array
                    if (names.Length > 0)
                    {
                        // Update the entry without "Juha"
                        Friends[Friends.IndexOf(friendEntry)] = string.Join(",", names);
                    }
                    else
                    {
                        // If there are no names left, remove the entire entry
                        Friends.Remove(friendEntry);
                    }

                    //return Ok($"{name} is removed from Friendlist.");
                    return Ok(Friends);
                }
            }

            return NotFound($"{name} not found in Friendlist.");
        }





    }
}

