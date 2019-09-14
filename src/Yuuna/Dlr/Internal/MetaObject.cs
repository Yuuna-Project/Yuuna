namespace Yuuna.Dlr.Internal
{
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq.Expressions;

    internal sealed class MetaObject : DynamicMetaObject
    {
        private readonly DynamicMetaObject _metaObject;

        internal MetaObject(IDynamicMetaObjectProvider provider, Expression exp, BindingRestrictions restrictions, object value) : base(exp, restrictions, value) => this._metaObject = provider.GetMetaObject(Expression.Constant(provider));

        public override DynamicMetaObject BindBinaryOperation(BinaryOperationBinder binder, DynamicMetaObject arg) => this._metaObject.BindBinaryOperation(binder, arg);

        public override DynamicMetaObject BindConvert(ConvertBinder binder) => this._metaObject.BindConvert(binder);

        public override DynamicMetaObject BindCreateInstance(CreateInstanceBinder binder, DynamicMetaObject[] args) => this._metaObject.BindCreateInstance(binder, args);

        public override DynamicMetaObject BindDeleteIndex(DeleteIndexBinder binder, DynamicMetaObject[] indexes) => this._metaObject.BindDeleteIndex(binder, indexes);

        public override DynamicMetaObject BindDeleteMember(DeleteMemberBinder binder) => this._metaObject.BindDeleteMember(binder);

        public override DynamicMetaObject BindGetIndex(GetIndexBinder binder, DynamicMetaObject[] indexes) => this._metaObject.BindGetIndex(binder, indexes);

        public override DynamicMetaObject BindGetMember(GetMemberBinder binder) => this._metaObject.BindGetMember(binder);

        public override DynamicMetaObject BindInvoke(InvokeBinder binder, DynamicMetaObject[] args) => this._metaObject.BindInvoke(binder, args);

        public override DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args) => this._metaObject.BindInvokeMember(binder, args);

        public override DynamicMetaObject BindSetIndex(SetIndexBinder binder, DynamicMetaObject[] indexes, DynamicMetaObject value) => this._metaObject.BindSetIndex(binder, indexes, value);

        public override DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value) => this._metaObject.BindSetMember(binder, value);

        public override DynamicMetaObject BindUnaryOperation(UnaryOperationBinder binder) => this._metaObject.BindUnaryOperation(binder);

        public override IEnumerable<string> GetDynamicMemberNames() => this._metaObject.GetDynamicMemberNames();
    }
}
