﻿using StardewValley;

namespace ExperienceConfig.External;

public interface ISpaceCoreApi
{
    string[] GetCustomSkills();
    void AddExperienceForCustomSkill(Farmer farmer, string skill, int amt);
}