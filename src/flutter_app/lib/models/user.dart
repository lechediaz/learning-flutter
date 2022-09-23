/// Represents the User.
class User {
  final int id;
  final String name;
  final String userName;
  final String roleName;

  const User({
    required this.id,
    required this.name,
    required this.userName,
    required this.roleName,
  });

  factory User.fromJson(Map<String, dynamic> json) {
    return User(
      id: json['id'] as int,
      name: json['name'] as String,
      userName: json['userName'] as String,
      roleName: json['roleName'] as String,
    );
  }
}
