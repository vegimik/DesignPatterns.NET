# Visitor:
## Its a pattern where a component (visitor) is allowed to traverse the entire inheritance hierarchy. Implemented by propagating a single visit() method throught the entire hierarchy.



# Dispatch:
## 1. Which function to call?
## 2. Single dispatch: depends on name of request and type of receiver
### 3. Double dispatch: depends on name of request and type of two receivers (type of visitor, type of element being visited)