using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;

using SharpSound.SoundCloud.Common;
using SharpSound.SoundCloud.Entities;

namespace SharpSound.SoundCloud.Query.Tracks
{
    public class TrackFinder : ExpressionVisitor
    {
        private List<LicenseEnum> licenses;
        private List<int> ids;

        private readonly Expression expression;

        public TrackFinder(Expression exp)
        {
            this.expression = exp;
        }

        public string BuildQuery()
        {
            this.licenses = new List<LicenseEnum>();
            this.ids = new List<int>();

            this.Visit(this.expression);

            var arguments = new Dictionary<string, string>();

            foreach (var license in this.licenses)
            {
                arguments.Add("license", license.GetAttributeOfType<EnumMemberAttribute>().Value);
            }

            if (this.ids.Any())
            {
                arguments.Add("ids", String.Join(",", this.ids));
            }
            
            return String.Join("&", arguments.Select(x => x.Key + "=" + x.Value));
        }

        protected override Expression VisitBinary(BinaryExpression be)
        {
            if (be.NodeType == ExpressionType.Equal)
            {
                if (ExpressionTreeHelpers.IsMemberEqualsValueExpression(be, typeof(Track), "id"))
                {
                    this.ids.Add(ExpressionTreeHelpers.GetValueFromEqualsExpression<int>(be, typeof(Track), "id"));
                    return be;
                }

                if (ExpressionTreeHelpers.IsMemberEqualsValueExpression(be, typeof(Track), "license"))
                {
                    this.licenses.Add(ExpressionTreeHelpers.GetValueFromEqualsExpression<LicenseEnum>(be, typeof(Track), "license"));
                    return be;
                }

                return base.VisitBinary(be);
            }

            return base.VisitBinary(be);
        }
    }
}