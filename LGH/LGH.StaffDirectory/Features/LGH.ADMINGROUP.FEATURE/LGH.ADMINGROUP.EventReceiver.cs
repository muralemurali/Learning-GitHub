using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using LGH.Model.Utility;

namespace LGH.StaffDirectory.Features.LGH.ADMINGROUP.FEATURE
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("2903b48a-3d21-4d0a-b0b7-0fcb7d8234e1")]
    public class LGHADMINGROUPEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        //public override void FeatureActivated(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        //public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}


        #region Variable Declaration

        string[] groupNames = { "LAFAYETTE GENERAL HEALTH ADMIN GROUP"};
        string[] permissionLevels = { "Full Control" };

        string groupDescription = "Lafayette General Health Administrators Group";

        #endregion

        #region Feature Activation

        /// <summary>
        /// Create New Group under the site
        /// </summary>
        /// <param name="properties"></param>
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            try
            {
                using (SPSite site = properties.Feature.Parent as SPSite)
                {

                    using (SPWeb web = site.RootWeb)
                    {
                        for (int i = 0; i < groupNames.Length; i++)
                        {
                            CreateSubSiteGroup(web, groupNames[i].ToString(), permissionLevels[i].ToString(), groupDescription);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("LGH WebParts", ex.Message);
            }
        }

        #endregion

        #region Feature De-Activation

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            try
            {
                using (SPSite site = properties.Feature.Parent as SPSite)
                {
                    using (SPWeb web =site.RootWeb)
                    {
                        for (int i = 0; i < groupNames.Length; i++)
                        {
                            DeleteSubSiteGroup(web, groupNames[i].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("LGH WebParts", ex.Message);
            }
        }

        #endregion

        #region Private Methods

        private static void CreateSubSiteGroup(SPWeb web, string groupName, string PermissionLevel, string groupDescription)
        {
            web.AllowUnsafeUpdates = true;

            SPUser owner = web.SiteAdministrators[0];
            SPMember member = web.SiteAdministrators[0];

            SPGroupCollection groups = web.SiteGroups;
            groups.Add(groupName, member, owner, groupDescription);

            SPGroup newSPGroup = groups[groupName];
            SPRoleDefinition role = web.RoleDefinitions[PermissionLevel];
            SPRoleAssignment roleAssignment = new SPRoleAssignment(newSPGroup);

            roleAssignment.RoleDefinitionBindings.Add(role);
            web.RoleAssignments.Add(roleAssignment);

            web.Update();
            web.AllowUnsafeUpdates = false;
        }

        private static void DeleteSubSiteGroup(SPWeb web, string groupName)
        {
            web.AllowUnsafeUpdates = true;

            SPGroupCollection groups = web.SiteGroups;
            groups.Remove(groupName);

            web.Update();
            web.AllowUnsafeUpdates = false;
        }

        #endregion
    }
}
