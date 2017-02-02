﻿//----------------------------------------------
// Flip Web Apps: Game Framework
// Copyright © 2016 Flip Web Apps / Mark Hewitt
//
// Please direct any bugs/comments/suggestions to http://www.flipwebapps.com
// 
// The copyright owner grants to the end user a non-exclusive, worldwide, and perpetual license to this Asset
// to integrate only as incorporated and embedded components of electronic games and interactive media and 
// distribute such electronic game and interactive media. End user may modify Assets. End user may otherwise 
// not reproduce, distribute, sublicense, rent, lease or lend the Assets. It is emphasized that the end 
// user shall not be entitled to distribute or transfer in any way (including, without, limitation by way of 
// sublicense) the Assets in any other way than as integrated components of electronic games and interactive media. 

// The above copyright notice and this permission notice must not be removed from any files.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//----------------------------------------------

using GameFramework.GameStructure.GameItems.ObjectModel;
using UnityEngine;

namespace GameFramework.GameStructure.GameItems.Components.AbstractClasses
{
    /// <summary>
    /// Creates instances of the passed prefab for all game items provided by the implementation class
    /// </summary>
    /// <typeparam name="TGameItemButton">The type of the Game Item button</typeparam>
    /// <typeparam name="TGameItem">The type of the Game Item</typeparam>
    public abstract class CreateGameItemButtons<TGameItemButton, TGameItem> : MonoBehaviour where TGameItemButton: GameItemButton<TGameItem> where TGameItem: GameItem, new()
    {
        /// <summary>
        /// A prefab that will be created for all GameItems.
        /// </summary>
        [Tooltip("A prefab that will be created for all GameItems.")]
        public GameObject Prefab;


        /// <summary>
        /// Create and add all buttons
        /// </summary>
        public void Awake()
        {
            foreach (var gameItem in GetGameItems())
            {
                var button = Prefab.GetComponent<TGameItemButton>();
                button.Number = gameItem.Number;

                var newObject = Instantiate(Prefab);
                newObject.transform.SetParent(transform, false);
            }
        }


        /// <summary>
        /// Implement this in your derived class to return an array of GameItems to create buttons for
        /// </summary>
        /// <returns></returns>
        protected abstract GameItem[] GetGameItems();
    }
}