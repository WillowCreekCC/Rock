using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rock.Model;
using Rock.Web.Cache;
using Xunit;

namespace Rock.Tests.Rock.Model
{
    public class GroupTypeTests
    {
        private EntityAttributes CreateEntityAttributes( string column, string value, params int[] attributeIds ) => new EntityAttributes
                                                                                                                     {
                                                                                                                         AttributeIds = attributeIds.ToList()
                                                                                                                       , EntityTypeQualifierColumn = column
                                                                                                                       , EntityTypeQualifierValue = value
                                                                                                                     };

        [Fact]
        public void GetGroupTypeAttributeIds_NoEntityAttributes()
        {
            var groupType = new GroupType { Id = 1 };

            var attributeIds = groupType.GetGroupTypeAttributeIds( new[] { groupType }, new List<EntityAttributes>() );

            Assert.Equal( 0, attributeIds.Count );
        }

        [Fact]
        public void GetGroupTypeAttributeIds_NoApplicableAttributes()
        {
            var groupType = new GroupType { Id = 1 };

            var entityAttributes = new List<EntityAttributes>
                                   {
                                       CreateEntityAttributes( "Id", "2", 1, 2 )
                                   };

            var attributeIds = groupType.GetGroupTypeAttributeIds( new[] { groupType }, entityAttributes );

            Assert.Equal( 0, attributeIds.Count );
        }

        [Fact]
        public void GetGroupTypeAttributeIds_NoInheritedGroupTypes()
        {
            var groupType = new GroupType { Id = 1 };

            var entityAttributes = new List<EntityAttributes>
                                   {
                                       CreateEntityAttributes( "Id", "1", 1, 2 )
                                   };

            var attributeIds = groupType.GetGroupTypeAttributeIds( new[] { groupType }, entityAttributes );

            Assert.Equal( 2, attributeIds.Count );
            Assert.True( attributeIds.Contains( 1 ) );
            Assert.True( attributeIds.Contains( 2 ) );
        }

        [Fact]
        public void GetGroupTypeAttributeIds_InheritedAttributes()
        {
            var inheritedGroupType = new GroupType { Id = 1 };
            var groupType = new GroupType { Id = 2, InheritedGroupTypeId = inheritedGroupType.Id };

            var entityAttributes = new List<EntityAttributes>
                                   {
                                       CreateEntityAttributes( "Id", inheritedGroupType.Id.ToString(), 1, 2 )
                                   };

            var attributeIds = groupType.GetGroupTypeAttributeIds( new[] { inheritedGroupType, groupType }, entityAttributes );

            Assert.Equal( 2, attributeIds.Count );
            Assert.True( attributeIds.Contains( 1 ) );
            Assert.True( attributeIds.Contains( 2 ) );
        }

        [Fact]
        public void GetGroupTypeAttributeIds_InheritedAndNonInheritedAttributes()
        {
            var inheritedGroupType = new GroupType { Id = 1 };
            var groupType = new GroupType { Id = 2, InheritedGroupTypeId = inheritedGroupType.Id };

            var entityAttributes = new List<EntityAttributes>
                                   {
                                       CreateEntityAttributes( "Id", inheritedGroupType.Id.ToString(), 1, 2 )
                                     , CreateEntityAttributes( "Id", groupType.Id.ToString(), 3, 4 )
                                   };

            var attributeIds = groupType.GetGroupTypeAttributeIds( new[] { inheritedGroupType, groupType }, entityAttributes );

            Assert.Equal( 4, attributeIds.Count );
            Assert.True( attributeIds.Contains( 1 ) );
            Assert.True( attributeIds.Contains( 2 ) );
            Assert.True( attributeIds.Contains( 3 ) );
            Assert.True( attributeIds.Contains( 4 ) );
        }

        [Fact]
        public void GetGroupTypeAttributeIds_InheritedAndNonInheritedAttributes2()
        {
            var inheritedGroupType = new GroupType { Id = 1 };
            var groupType = new GroupType { Id = 2, InheritedGroupTypeId = inheritedGroupType.Id };

            var entityAttributes = new List<EntityAttributes>
                                   {
                                       CreateEntityAttributes( "Id", inheritedGroupType.Id.ToString(), 1, 2 )
                                     , CreateEntityAttributes( "Id", groupType.Id.ToString(), 3, 4 )
                                     , CreateEntityAttributes( "Id", "999", 1000, 1001 )
                                   };

            var attributeIds = groupType.GetGroupTypeAttributeIds( new[] { inheritedGroupType, groupType }, entityAttributes );

            Assert.Equal( 4, attributeIds.Count );
            Assert.True( attributeIds.Contains( 1 ) );
            Assert.True( attributeIds.Contains( 2 ) );
            Assert.True( attributeIds.Contains( 3 ) );
            Assert.True( attributeIds.Contains( 4 ) );
        }

        [Fact]
        public void GetGroupTypeAttributeIds_InheritTwoLevels()
        {
            var inheritedGroupType = new GroupType { Id = 1 };
            var inheritedGroupType2 = new GroupType { Id = 2, InheritedGroupTypeId = inheritedGroupType.Id };
            var groupType = new GroupType { Id = 3, InheritedGroupTypeId = inheritedGroupType2.Id };

            var entityAttributes = new List<EntityAttributes>
                                   {
                                       CreateEntityAttributes( "Id", inheritedGroupType.Id.ToString(), 1, 2 )
                                     , CreateEntityAttributes( "Id", inheritedGroupType2.Id.ToString(), 3, 4 )
                                     , CreateEntityAttributes( "Id", groupType.Id.ToString(), 5, 6 )
                                   };

            var attributeIds = groupType.GetGroupTypeAttributeIds( new[] { inheritedGroupType, inheritedGroupType2, groupType }, entityAttributes );

            Assert.Equal( 6, attributeIds.Count );
            Assert.True( attributeIds.Contains( 1 ) );
            Assert.True( attributeIds.Contains( 2 ) );
            Assert.True( attributeIds.Contains( 3 ) );
            Assert.True( attributeIds.Contains( 4 ) );
            Assert.True( attributeIds.Contains( 5 ) );
            Assert.True( attributeIds.Contains( 6 ) );
        }

        [Fact]
        public void GetGroupTypeAttributeIds_InheritedPurposeAttributes()
        {
            var inheritedGroupType = new GroupType { Id = 1, GroupTypePurposeValueId = 500 };
            var groupType = new GroupType { Id = 2, InheritedGroupTypeId = inheritedGroupType.Id };

            var entityAttributes = new List<EntityAttributes>
                                   {
                                       CreateEntityAttributes( "GroupTypePurposeValueId", inheritedGroupType.GroupTypePurposeValueId.ToString(), 1, 2 )
                                     , CreateEntityAttributes( "GroupTypePurposeValueId", "999", 3, 4 )
                                   };

            var attributeIds = groupType.GetGroupTypeAttributeIds( new[] { inheritedGroupType, groupType }, entityAttributes );

            Assert.Equal( 2, attributeIds.Count );
            Assert.True( attributeIds.Contains( 1 ) );
            Assert.True( attributeIds.Contains( 2 ) );
        }
    }
}
