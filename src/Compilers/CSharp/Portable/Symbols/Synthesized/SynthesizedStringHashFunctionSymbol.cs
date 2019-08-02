// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CodeGen;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
    /// <summary>
    /// Represents a compiler generated synthesized method symbol
    /// representing string switch hash function
    /// </summary>
    internal sealed partial class SynthesizedStringSwitchHashMethod : SynthesizedGlobalMethodSymbol
    {
        internal SynthesizedStringSwitchHashMethod(SourceModuleSymbol containingModule, PrivateImplementationDetails privateImplType, TypeSymbol returnType, TypeSymbol paramType)
            : base(containingModule, privateImplType, returnType, PrivateImplementationDetails.SynthesizedStringHashFunctionName)
        {
            this.SetParameters(ImmutableArray.Create<ParameterSymbol>(SynthesizedParameterSymbol.Create(this, TypeWithAnnotations.Create(paramType), 0, RefKind.None, "s")));
        }
    }

    /// <summary>
    /// Represents a compiler generated synthesized method symbol
    /// representing string switch hash function
    /// </summary>
    internal sealed partial class ThrowIfNullMethod : SynthesizedGlobalMethodSymbol
    {
        internal ThrowIfNullMethod(SourceModuleSymbol containingModule, PrivateImplementationDetails privateImplType, TypeSymbol returnType, TypeSymbol paramType)
            : base(containingModule, privateImplType, returnType, WellKnownMemberNames.ThrowIfNullMethodName)
        {
            this.SetParameters(ImmutableArray.Create<ParameterSymbol>(SynthesizedParameterSymbol.Create(this, TypeWithAnnotations.Create(paramType), 0, RefKind.None, name: "name")));
        }

        internal override void GenerateMethodBody(TypeCompilationState compilationState, DiagnosticBag diagnostics)
        {
            var factory = new SyntheticBoundNodeFactory(this, this.GetNonNullSyntaxNode(), compilationState, diagnostics);
            factory.CurrentFunction = this;
            var argumentName = ImmutableArray.Create<BoundExpression>(factory.StringLiteral(this.Parameters[0].Name));
            BoundObjectCreationExpression ex = factory.New(factory.WellKnownMethod(WellKnownMember.System_ArgumentNullException__ctorString), argumentName);
            var throwArgNullStatement = factory.Throw(ex);
            factory.CloseMethod(throwArgNullStatement);
        }
    }
}
