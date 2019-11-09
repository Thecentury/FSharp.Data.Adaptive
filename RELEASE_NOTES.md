### 0.0.5-prerelease08
* added AdaptiveReductions
* AList.reduce/reduceBy/reduceByA
* AList.count/isEmpty

### 0.0.5-prerelease07
* AList.mapA/chooseA/filterA

### 0.0.5-prerelease06
* Fable compat
    * ConditionalWeakTable polyfill
    * fixed Interlocked.Increment in Callbacks for Fable

### 0.0.5-prerelease05
* callback optimizations (single CallbackObject for many callbacks)

### 0.0.5-prerelease04
* AVal.bind3
* added non-generic AdaptiveValue interface
* implemented Bind<N>Return/Bind<N> for adaptive builder

### 0.0.5-prerelease03
* IndexList.tryGetPosition

### 0.0.5-prerelease02
* IndexListDelta.ofIndexList
* IndexList.tryRemove IndexList.neighbours

### 0.0.5-prerelease01
* added ConservativeEquals / UpdateTo to HashMap/IndexList/etc.

### 0.0.4
* IndexList.choose2 / toSeqIndexed / etc.

### 0.0.3
* relaxed FSharp.Core version

### 0.0.2
fixed fable package

### 0.0.1
added several missing operators like
* `AList.ofAVal`
* `AMap.fold(Group|HalfGroup)`
* `AMap.tryFind`
* `AList.try(Get|At)`
* `AMap.ofASet`

### 0.0.1-prerelease01
initial version