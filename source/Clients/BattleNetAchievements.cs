﻿using CommonPlayniteShared.PluginLibrary.BattleNetLibrary.Models;
using Playnite.SDK;
using Playnite.SDK.Data;
using System;

namespace SuccessStory.Clients
{
    abstract class BattleNetAchievements : GenericAchievements
    {
        protected IWebView WebViewOffscreen;

        protected const string apiStatusUrl = @"https://account.blizzard.com/api/";

        public BattleNetAchievements() : base()
        {
            WebViewOffscreen = PluginDatabase.PlayniteApi.WebViews.CreateOffscreenView();
        }

        public override bool IsConfigured()
        {
            throw new NotImplementedException();
        }

        protected BattleNetApiStatus GetApiStatus()
        {
            try
            {
                // This refreshes authentication cookie
                WebViewOffscreen.NavigateAndWait("https://account.blizzard.com:443/oauth2/authorization/account-settings");
                WebViewOffscreen.NavigateAndWait(apiStatusUrl);
                var textStatus = WebViewOffscreen.GetPageText();
                return Serialization.FromJson<BattleNetApiStatus>(textStatus);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }


    class ColorElement
    {
        public string Name { get; set; }
        public string Color { get; set; }
    }
}