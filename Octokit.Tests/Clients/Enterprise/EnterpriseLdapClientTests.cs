﻿using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class EnterpriseLdapClientTests
    {
        public class TheUpdateUserMappingMethod
        {
            readonly string _distinguishedNameUser = "uid=test-user,ou=users,dc=company,dc=com";

            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseLdapClient(connection);

                string expectedUri = "admin/ldap/users/test-user/mapping";
                client.UpdateUserMapping("test-user", new NewLdapMapping(_distinguishedNameUser));

                connection.Received().Patch<LdapUser>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public void PassesRequestObject()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseLdapClient(connection);

                client.UpdateUserMapping("test-user", new NewLdapMapping(_distinguishedNameUser));

                connection.Received().Patch<LdapUser>(
                    Arg.Any<Uri>(),
                    Arg.Is<NewLdapMapping>(a =>
                        a.LdapDN == _distinguishedNameUser));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseLdapClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateUserMapping(null, new NewLdapMapping(_distinguishedNameUser)));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateUserMapping("test-user", null));
            }
        }

        public class TheQueueSyncUserMappingMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseLdapClient(connection);

                string expectedUri = "admin/ldap/users/test-user/sync";
                client.QueueSyncUserMapping("test-user");

                connection.Received().Post<LdapSyncResponse>(
                    Arg.Is<Uri>(u => u.ToString() == expectedUri),
                    Arg.Any<object>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseLdapClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.QueueSyncUserMapping(null));
            }
        }

        public class TheUpdateTeamMappingMethod
        {
            readonly string _distinguishedNameTeam = "uid=DG-Test-Team,ou=groups,dc=company,dc=com";

            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseLdapClient(connection);

                string expectedUri = "admin/ldap/teams/1/mapping";
                client.UpdateTeamMapping(1, new NewLdapMapping(_distinguishedNameTeam));

                connection.Received().Patch<LdapTeam>(
                    Arg.Is<Uri>(u => u.ToString() == expectedUri),
                    Arg.Any<object>());
            }

            [Fact]
            public void PassesRequestObject()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseLdapClient(connection);

                client.UpdateTeamMapping(1, new NewLdapMapping(_distinguishedNameTeam));

                connection.Received().Patch<LdapTeam>(
                    Arg.Any<Uri>(),
                    Arg.Is<NewLdapMapping>(a =>
                        a.LdapDN == _distinguishedNameTeam));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseLdapClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateTeamMapping(1, null));
            }
        }

        public class TheQueueSyncTeamMappingMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseLdapClient(connection);

                string expectedUri = "admin/ldap/teams/1/sync";
                client.QueueSyncTeamMapping(1);

                connection.Received().Post<LdapSyncResponse>(
                    Arg.Is<Uri>(u => u.ToString() == expectedUri),
                    Arg.Any<object>());
            }
        }
    }
}
