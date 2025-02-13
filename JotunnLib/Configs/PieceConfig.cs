﻿using Jotunn.Entities;
using UnityEngine;

namespace Jotunn.Configs
{
    /// <summary>
    ///     Configuration class for adding custom pieces.<br />
    ///     Use this in a constructor of <see cref="CustomPiece"/> and 
    ///     Jötunn resolves the references to the game objects at runtime.
    /// </summary>
    public class PieceConfig
    {
        /// <summary>
        ///     The description of your piece.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        ///     Whether this piece is buildable or not.
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        ///     Can this piece be built in dungeons?
        /// </summary>
        public bool AllowedInDungeons { get; set; } = false;

        /// <summary>
        ///     The name of the piece table where this piece will be added.
        /// </summary>
        public string PieceTable { get; set; } = string.Empty;

        /// <summary>
        ///     The name of the crafting station prefab which needs to be in close proximity to build this piece.
        /// </summary>
        public string CraftingStation { get; set; } = string.Empty;

        /// <summary>
        ///     The name of the crafting station prefab to which this piece will be an upgrade to.
        /// </summary>
        public string ExtendStation { get; set; } = string.Empty;
        
        /// <summary>
        ///     Icon which is displayed in the crafting GUI.
        /// </summary>
        public Sprite Icon { get; set; } = null;

        /// <summary>
        ///     Array of <see cref="RequirementConfig"/>s for all crafting materials it takes to craft the recipe.
        /// </summary>
        public RequirementConfig[] Requirements { get; set; } = new RequirementConfig[0];

        /// <summary>
        ///     Converts the RequirementConfigs to Valheim style Piece.Requirements
        /// </summary>
        /// <returns>The Valheim Piece.Requirement array</returns>
        public Piece.Requirement[] GetRequirements()
        {
            Piece.Requirement[] reqs = new Piece.Requirement[Requirements.Length];

            for (int i = 0; i < reqs.Length; i++)
            {
                reqs[i] = Requirements[i].GetRequirement();
            }

            return reqs;
        }

        /// <summary>
        ///     Apply this configs values to a piece GameObject.
        /// </summary>
        /// <param name="prefab"></param>
        public void Apply(GameObject prefab)
        {
            var piece = prefab.GetComponent<Piece>();
            if (piece == null)
            {
                Logger.LogWarning($"GameObject has no Piece attached");
                return;
            }

            piece.enabled = Enabled;
            piece.m_allowedInDungeons = AllowedInDungeons;
            
            // Set icon if overriden
            if (Icon != null)
            {
                piece.m_icon = Icon;
            }

            // Assign all needed resources for this piece
            piece.m_resources = GetRequirements();

            // Assign the CraftingStation for this piece
            if (!string.IsNullOrEmpty(CraftingStation))
            {
                piece.m_craftingStation = Mock<CraftingStation>.Create(CraftingStation);
            }

            // Assign an extension station for this piece
            if (!string.IsNullOrEmpty(ExtendStation))
            {
                var stationExt = prefab.GetComponent<StationExtension>();
                if (stationExt == null)
                {
                    stationExt = prefab.AddComponent<StationExtension>();
                }
                
                stationExt.m_craftingStation = Mock<CraftingStation>.Create(ExtendStation);
            }
        }
    }
}
