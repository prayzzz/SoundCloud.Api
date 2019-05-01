Breaking Changes:
* per Id methods now require a `long` instead of `int`
* `Id` in Entities are now `long` too
* All search / list methods have proper support for `linked_partitioning` now
  * `offset` got removed from QueryBuilders
* `PlaylistQueryBuilder` no longer requires a search string.