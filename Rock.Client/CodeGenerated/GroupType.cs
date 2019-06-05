//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the Rock.CodeGeneration project
//     Changes to this file will be lost when the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
// <copyright>
// Copyright by the Spark Development Network
//
// Licensed under the Rock Community License (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.rockrms.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//
using System;
using System.Collections.Generic;


namespace Rock.Client
{
    /// <summary>
    /// Base client model for GroupType that only includes the non-virtual fields. Use this for PUT/POSTs
    /// </summary>
    public partial class GroupTypeEntity
    {
        /// <summary />
        public int Id { get; set; }

        /// <summary />
        public string AdministratorTerm { get; set; } = @"Administrator";

        /// <summary />
        public Rock.Client.Enums.ScheduleType AllowedScheduleTypes { get; set; }

        /// <summary />
        public bool AllowGroupSync { get; set; }

        /// <summary />
        public bool AllowMultipleLocations { get; set; }

        /// <summary />
        public bool AllowSpecificGroupMemberAttributes { get; set; }

        /// <summary />
        public bool AllowSpecificGroupMemberWorkflows { get; set; }

        /// <summary />
        public bool AttendanceCountsAsWeekendService { get; set; }

        /// <summary />
        public Rock.Client.Enums.PrintTo AttendancePrintTo { get; set; }

        /// <summary />
        public Rock.Client.Enums.AttendanceRule AttendanceRule { get; set; }

        /// <summary />
        public int? DefaultGroupRoleId { get; set; }

        /// <summary />
        public string Description { get; set; }

        /// <summary />
        public bool EnableGroupHistory { get; set; }

        /// <summary />
        public bool EnableGroupTag { get; set; }

        /// <summary />
        public bool? EnableLocationSchedules { get; set; }

        /// <summary />
        public bool EnableSpecificGroupRequirements { get; set; }

        /// <summary />
        public Guid? ForeignGuid { get; set; }

        /// <summary />
        public string ForeignKey { get; set; }

        /// <summary />
        public bool GroupAttendanceRequiresLocation { get; set; }

        /// <summary />
        public bool GroupAttendanceRequiresSchedule { get; set; }

        /// <summary />
        public Rock.Client.Enums.GroupCapacityRule GroupCapacityRule { get; set; }

        /// <summary />
        public string GroupMemberTerm { get; set; } = @"Member";

        /// <summary />
        public bool GroupsRequireCampus { get; set; }

        /// <summary />
        public DefinedType GroupStatusDefinedType { get; set; }

        /// <summary />
        public int? GroupStatusDefinedTypeId { get; set; }

        /// <summary />
        public string GroupTerm { get; set; } = @"Group";

        /// <summary />
        public string GroupTypeColor { get; set; }

        /// <summary />
        public int? GroupTypePurposeValueId { get; set; }

        /// <summary />
        public string GroupViewLavaTemplate { get; set; } = @"{% if Group.GroupType.GroupCapacityRule != 'None' and  Group.GroupCapacity != '' %}
		{% assign warningLevel = ''warning'' %}

		{% if Group.GroupType.GroupCapacityRule == 'Hard' %}
			{% assign warningLevel = 'danger' %}
		{% endif %}

		{% assign activeMemberCount = countActive | Plus:1 %} {% comment %}the counter is zero based{% endcomment %}
		{% assign overageAmount = activeMemberCount | Minus:Group.GroupCapacity %}

		{% if overageAmount > 0 %}
			<div class=""alert alert-{{ warningLevel }} margin-t-sm"">This group is over capacity by {{ overageAmount }} {{ 'individual' | PluralizeForQuantity:overageAmount }}.</div>
		{% endif %}
	{% endif %}
	
	
	
{% if Group.Description != '' -%}
    <p class='description'>{{ Group.Description }}</p>
{% endif -%}

<div class=""row"">
   <div class=""col-md-6"">
        <dl>
            {% if Group.ParentGroup != null %}
            <dt> Parent Group </ dt>
               <dd>{{ Group.ParentGroup.Name }}</dd>
            {% endif %}
            {% if Group.RequiredSignatureDocumentTemplate != null %}
            <dt> Required Signed Document </dt>
               <dd>{{ Group.RequiredSignatureDocumentTemplate.Name }}</ dd >
            {% endif %}
            {% if Group.Schedule != null %}

            <dt> Schedule </dt>
            <dd>{{ Group.Schedule.FriendlyScheduleText }}</ dd >
            {% endif %}
            {% if Group.GroupCapacity != null and Group.GroupCapacity != '' %}

            <dt> Capacity </dt>

            <dd>{{ Group.GroupCapacity }}</dd>
            {% endif %}
        {% if Group.GroupType.ShowAdministrator and Group.GroupAdministratorPersonAlias != null and Group.GroupAdministratorPersonAlias != '' %}
            <dt> {{ Group.GroupType.AdministratorTerm }}</dt>
            <dd>{{ Group.GroupAdministratorPersonAlias.Person.FullName }}</dd>
            {% endif %}
        </dl>
        <dl>
        {% for attribute in Group.AttributeValues %}
        <dt>{{ attribute.AttributeName }}:</dt>

<dd>{{ attribute.ValueFormatted }} </dd>
        {% endfor %}
        </dl>
    </div>

    <div class=""col-md-6 location-maps"">
	{% assign googleAPIKey = 'Global' | Attribute: 'GoogleAPIKey' %}
	{% assign staticMapStyle = MapStyle | Attribute: 'StaticMapStyle' %}

	{% if Group.GroupLocations != null %}
	{% assign groupLocations = Group.GroupLocations %}
	{% assign locationCount = groupLocations | Size %}
	    {% if locationCount > 0 and googleAPIKey != null and googleAPIKey !='' and staticMapStyle != null and staticMapStyle != '' %}
		{% for groupLocation in groupLocations %}
	    	{% if groupLocation.Location.GeoPoint != null and groupLocation.Location.GeoPoint != '' %}
	    	{% capture markerPoints %}{{ groupLocation.Location.Latitude }},{{ groupLocation.Location.Longitude }}{% endcapture %}
	    	{% assign mapLink = staticMapStyle | Replace:'{MarkerPoints}', markerPoints   %}
	    	{% assign mapLink = mapLink | Replace:'{PolygonPoints}','' %}
	    	{% assign mapLink = mapLink | Append:'&sensor=false&size=450x250&zoom=13&format=png&key=' %}
            {% assign mapLink = mapLink | Append: googleAPIKey %}
	    	<div class=""group-location-map"">
	    	    {% if groupLocation.GroupLocationTypeValue != null %}
	    	    <h4> {{ groupLocation.GroupLocationTypeValue.Value }} </h4>
	    	    {% endif %}
	    	    <a href = '{{ GroupMapUrl }}'>
	    	    <img class='img-thumbnail' src='{{ mapLink }}'/>
	    	    </a>
	    	    {% if groupLocation.Location.FormattedAddress != null and groupLocation.Location.FormattedAddress != '' and ShowLocationAddresses == true %}
	    	    {{ groupLocation.Location.FormattedAddress }}
	    	    {% endif %}
	    	 </div>
		    {% endif %}
		    {% if groupLocation.Location.GeoFence != null and groupLocation.Location.GeoFence != ''  %}

		    {% assign mapLink = staticMapStyle | Replace:'{MarkerPoints}','' %}
		    {% assign googlePolygon = 'enc:' | Append: groupLocation.Location.GooglePolygon %}
	    	{% assign mapLink = mapLink | Replace:'{PolygonPoints}', googlePolygon  %}
	    	{% assign mapLink = mapLink | Append:'&sensor=false&size=350x200&format=png&key=' %}
	    	{% assign mapLink = mapLink | Append: googleAPIKey %}
		    <div class='group-location-map'>
		        {% if groupLocation.GroupLocationTypeValue != null %}
		        <h4> {{ groupLocation.GroupLocationTypeValue.Value }} </h4>
		        {% endif %}
		    <a href = '{{ GroupMapUrl }}'><img class='img-thumbnail' src='{{ mapLink }}'/></a>
		    </div>	
		    {% endif %}
		{% endfor %}
		{% endif %}
	{% endif %}
	{% if Group.Linkages != null %}
	{% assign linkages = Group.Linkages %}
	{% assign linkageCount = linkages | Size %}
	{% if linkageCount > 0 %}
	{% assign countRegistration = 0 %}
	{% assign countLoop = 0 %}
	{% assign countEventItemOccurrences = 0 %}
	{% assign countContentItems = 0 %}
	{% for linkage in linkages %}
		{% if linkage.RegistrationInstanceId != null and linkage.RegistrationInstanceId != '' %}
			{% if countRegistration == 0 %}
			<strong> Registrations</strong>
			<ul class=""list-unstyled"">
			{% endif %}
			<li><a href = '{{ RegistrationInstancePage }}?RegistrationInstanceId={{ linkage.RegistrationInstanceId }}'>{% if linkage.EventItemOccurrence != null %} {{ linkage.EventItemOccurrence.EventItem.Name }} ({% if linkage.EventItemOccurrence.Campus != null %} {{ linkage.EventItemOccurrence.Campus.Name }}  {% else %}  All Campuses {% endif %}) {% endif %} - {{ linkage.RegistrationInstance.Name }}</a></li>
			{% assign countRegistration = countRegistration | Plus: 1 %}
		{% endif %}
		{% assign countLoop = countLoop | Plus: 1 %}
		{% if countRegistration > 0 and countLoop == linkageCount  %}
		</ul>
		{% endif %}
	{% endfor %}
	{% assign countLoop = 0 %}
	{% for linkage in linkages %}
		{% if linkage.EventItemOccurrence != null and linkage.EventItemOccurrence.EventItem != null %}
			{% if countEventItemOccurrences == 0 %}
			<strong> Event Item Occurrences</strong>
			<ul class=""list-unstyled"">
			{% endif %}
			<li><a href = '{{ EventItemOccurrencePage }}?EventItemOccurrenceId={{ linkage.EventItemOccurrence.Id }}'>{% if linkage.EventItemOccurrence != null %} {{ linkage.EventItemOccurrence.EventItem.Name }} -{% if linkage.EventItemOccurrence.Campus != null %} {{ linkage.EventItemOccurrence.Campus.Name }}  {% else %}  All Campuses {% endif %} {% endif %}</a></li>
			{% assign countEventItemOccurrences = countEventItemOccurrences | Plus: 1 %}
		{% endif %}
		{% assign countLoop = countLoop | Plus: 1 %}
		{% if countEventItemOccurrences > 0  and countLoop == linkageCount %}
			</ul>
		{% endif %}
	{% endfor %}
	{% assign countLoop = 0 %}
	{% for linkage in linkages %}
		{% if linkage.EventItemOccurrence != null and linkage.EventItemOccurrence.EventItem != null %}
			{% assign contentChannelItemsCount = linkage.EventItemOccurrence.ContentChannelItems | Size %}
			{% if contentChannelItemsCount > 0 %}
			{% assign contentChannelItems = linkage.EventItemOccurrence.ContentChannelItems %}
				{% for contentChannelItem in contentChannelItems %}
				{% if contentChannelItem.ContentChannelItem != null  %}
					{% if countContentItems == 0 %}
					<strong> Content Items</strong>
					<ul class=""list-unstyled"">
					{% endif %}
					<li><a href = '{{ ContentItemPage }}?ContentItemId={{ contentChannelItem.ContentChannelItemId }}'>{{ contentChannelItem.ContentChannelItem.Title }} <small>({{ contentChannelItem.ContentChannelItem.ContentChannelType.Name }})</small></a></li>
					{% assign countContentItems = countContentItems | Plus: 1 %}
				{% endif %}
				{% endfor %}
			{% endif %}
    	{% endif %}
    	{% assign countLoop = countLoop | Plus: 1 %}
    	{% if countContentItems > 0 and countLoop == linkageCount %}
			</ul>
		{% endif %}
	{% endfor %}
	{% endif %}
{% endif %}
	</div>
</div>";

        /// <summary />
        public string IconCssClass { get; set; }

        /// <summary />
        public bool IgnorePersonInactivated { get; set; }

        /// <summary />
        public int? InheritedGroupTypeId { get; set; }

        /// <summary />
        public bool IsIndexEnabled { get; set; }

        /// <summary />
        public bool IsSchedulingEnabled { get; set; }

        /// <summary />
        public bool IsSystem { get; set; }

        /// <summary />
        public Rock.Client.Enums.GroupLocationPickerMode LocationSelectionMode { get; set; }

        /// <summary>
        /// If the ModifiedByPersonAliasId is being set manually and should not be overwritten with current user when saved, set this value to true
        /// </summary>
        public bool ModifiedAuditValuesAlreadyUpdated { get; set; }

        /// <summary />
        public string Name { get; set; }

        /// <summary />
        public int Order { get; set; }

        /// <summary />
        public bool RequiresReasonIfDeclineSchedule { get; set; }

        /// <summary />
        public int? ScheduleCancellationWorkflowTypeId { get; set; }

        /// <summary />
        public int? ScheduleConfirmationEmailOffsetDays { get; set; } = 4;

        /// <summary />
        public int? ScheduleConfirmationSystemEmailId { get; set; }

        /// <summary />
        public int? ScheduleReminderEmailOffsetDays { get; set; } = 2;

        /// <summary />
        public int? ScheduleReminderSystemEmailId { get; set; }

        /// <summary />
        public bool SendAttendanceReminder { get; set; }

        /// <summary />
        public bool ShowAdministrator { get; set; }

        /// <summary />
        public bool ShowConnectionStatus { get; set; }

        /// <summary />
        public bool ShowInGroupList { get; set; } = true;

        /// <summary />
        public bool ShowInNavigation { get; set; } = true;

        /// <summary />
        public bool ShowMaritalStatus { get; set; }

        /// <summary />
        public bool TakesAttendance { get; set; }

        /// <summary>
        /// Leave this as NULL to let Rock set this
        /// </summary>
        public DateTime? CreatedDateTime { get; set; }

        /// <summary>
        /// This does not need to be set or changed. Rock will always set this to the current date/time when saved to the database.
        /// </summary>
        public DateTime? ModifiedDateTime { get; set; }

        /// <summary>
        /// Leave this as NULL to let Rock set this
        /// </summary>
        public int? CreatedByPersonAliasId { get; set; }

        /// <summary>
        /// If you need to set this manually, set ModifiedAuditValuesAlreadyUpdated=True to prevent Rock from setting it
        /// </summary>
        public int? ModifiedByPersonAliasId { get; set; }

        /// <summary />
        public Guid Guid { get; set; }

        /// <summary />
        public int? ForeignId { get; set; }

        /// <summary>
        /// Copies the base properties from a source GroupType object
        /// </summary>
        /// <param name="source">The source.</param>
        public void CopyPropertiesFrom( GroupType source )
        {
            this.Id = source.Id;
            this.AdministratorTerm = source.AdministratorTerm;
            this.AllowedScheduleTypes = source.AllowedScheduleTypes;
            this.AllowGroupSync = source.AllowGroupSync;
            this.AllowMultipleLocations = source.AllowMultipleLocations;
            this.AllowSpecificGroupMemberAttributes = source.AllowSpecificGroupMemberAttributes;
            this.AllowSpecificGroupMemberWorkflows = source.AllowSpecificGroupMemberWorkflows;
            this.AttendanceCountsAsWeekendService = source.AttendanceCountsAsWeekendService;
            this.AttendancePrintTo = source.AttendancePrintTo;
            this.AttendanceRule = source.AttendanceRule;
            this.DefaultGroupRoleId = source.DefaultGroupRoleId;
            this.Description = source.Description;
            this.EnableGroupHistory = source.EnableGroupHistory;
            this.EnableGroupTag = source.EnableGroupTag;
            this.EnableLocationSchedules = source.EnableLocationSchedules;
            this.EnableSpecificGroupRequirements = source.EnableSpecificGroupRequirements;
            this.ForeignGuid = source.ForeignGuid;
            this.ForeignKey = source.ForeignKey;
            this.GroupAttendanceRequiresLocation = source.GroupAttendanceRequiresLocation;
            this.GroupAttendanceRequiresSchedule = source.GroupAttendanceRequiresSchedule;
            this.GroupCapacityRule = source.GroupCapacityRule;
            this.GroupMemberTerm = source.GroupMemberTerm;
            this.GroupsRequireCampus = source.GroupsRequireCampus;
            this.GroupStatusDefinedType = source.GroupStatusDefinedType;
            this.GroupStatusDefinedTypeId = source.GroupStatusDefinedTypeId;
            this.GroupTerm = source.GroupTerm;
            this.GroupTypeColor = source.GroupTypeColor;
            this.GroupTypePurposeValueId = source.GroupTypePurposeValueId;
            this.GroupViewLavaTemplate = source.GroupViewLavaTemplate;
            this.IconCssClass = source.IconCssClass;
            this.IgnorePersonInactivated = source.IgnorePersonInactivated;
            this.InheritedGroupTypeId = source.InheritedGroupTypeId;
            this.IsIndexEnabled = source.IsIndexEnabled;
            this.IsSchedulingEnabled = source.IsSchedulingEnabled;
            this.IsSystem = source.IsSystem;
            this.LocationSelectionMode = source.LocationSelectionMode;
            this.ModifiedAuditValuesAlreadyUpdated = source.ModifiedAuditValuesAlreadyUpdated;
            this.Name = source.Name;
            this.Order = source.Order;
            this.RequiresReasonIfDeclineSchedule = source.RequiresReasonIfDeclineSchedule;
            this.ScheduleCancellationWorkflowTypeId = source.ScheduleCancellationWorkflowTypeId;
            this.ScheduleConfirmationEmailOffsetDays = source.ScheduleConfirmationEmailOffsetDays;
            this.ScheduleConfirmationSystemEmailId = source.ScheduleConfirmationSystemEmailId;
            this.ScheduleReminderEmailOffsetDays = source.ScheduleReminderEmailOffsetDays;
            this.ScheduleReminderSystemEmailId = source.ScheduleReminderSystemEmailId;
            this.SendAttendanceReminder = source.SendAttendanceReminder;
            this.ShowAdministrator = source.ShowAdministrator;
            this.ShowConnectionStatus = source.ShowConnectionStatus;
            this.ShowInGroupList = source.ShowInGroupList;
            this.ShowInNavigation = source.ShowInNavigation;
            this.ShowMaritalStatus = source.ShowMaritalStatus;
            this.TakesAttendance = source.TakesAttendance;
            this.CreatedDateTime = source.CreatedDateTime;
            this.ModifiedDateTime = source.ModifiedDateTime;
            this.CreatedByPersonAliasId = source.CreatedByPersonAliasId;
            this.ModifiedByPersonAliasId = source.ModifiedByPersonAliasId;
            this.Guid = source.Guid;
            this.ForeignId = source.ForeignId;

        }
    }

    /// <summary>
    /// Client model for GroupType that includes all the fields that are available for GETs. Use this for GETs (use GroupTypeEntity for POST/PUTs)
    /// </summary>
    public partial class GroupType : GroupTypeEntity
    {
        /// <summary />
        public ICollection<GroupType> ChildGroupTypes { get; set; }

        /// <summary />
        public GroupTypeRole DefaultGroupRole { get; set; }

        /// <summary />
        public ICollection<GroupRequirement> GroupRequirements { get; set; }

        /// <summary />
        public DefinedValue GroupTypePurposeValue { get; set; }

        /// <summary />
        public ICollection<GroupTypeLocationType> LocationTypes { get; set; }

        /// <summary />
        public ICollection<GroupTypeRole> Roles { get; set; }

        /// <summary />
        public WorkflowType ScheduleCancellationWorkflowType { get; set; }

        /// <summary />
        public SystemEmail ScheduleConfirmationSystemEmail { get; set; }

        /// <summary />
        public SystemEmail ScheduleReminderSystemEmail { get; set; }

        /// <summary>
        /// NOTE: Attributes are only populated when ?loadAttributes is specified. Options for loadAttributes are true, false, 'simple', 'expanded' 
        /// </summary>
        public Dictionary<string, Rock.Client.Attribute> Attributes { get; set; }

        /// <summary>
        /// NOTE: AttributeValues are only populated when ?loadAttributes is specified. Options for loadAttributes are true, false, 'simple', 'expanded' 
        /// </summary>
        public Dictionary<string, Rock.Client.AttributeValue> AttributeValues { get; set; }
    }
}
