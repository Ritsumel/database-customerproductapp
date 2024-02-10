using ConsoleApp.Entities;
using ConsoleApp.Repositories;

namespace ConsoleApp.Services;

internal class RoleService
{
    private readonly RoleRepository _roleRepository;

    public RoleService(RoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }



    //CREATE
    public RoleEntity CreateRole(string roleName)
    {
        var roleEntity = _roleRepository.Get(x => x.RoleName == roleName);
        roleEntity ??= _roleRepository.Create(new RoleEntity { RoleName = roleName });

        return roleEntity;
    }

    //READ
    public RoleEntity GetRoleByRoleName(string roleName)
    {
        var roleEntity = _roleRepository.Get(x => x.RoleName == roleName);
        return roleEntity;
    }

    public RoleEntity GetRoleById(int id)
    {
        var roleEntity = _roleRepository.Get(x => x.Id == id);
        return roleEntity;
    }

    public IEnumerable<RoleEntity> GetCategories()
    {
        var categories = _roleRepository.GetAll();
        return categories;
    }

    //UPDATE
    public RoleEntity UpdateRole(RoleEntity roleEntity)
    {
        var updatedRoleEntity = _roleRepository.Update(x => x.Id == roleEntity.Id, roleEntity);
        return updatedRoleEntity;
    }

    //DELETE
    public void DeleteRole(int id)
    {
        _roleRepository.Delete(x => x.Id == id);
    }




}
